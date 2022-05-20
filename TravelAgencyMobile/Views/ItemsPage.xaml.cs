using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TravelAgency;
using TravelAgencyMobile.Models;
using TravelAgencyMobile.ViewModels;
using TravelAgencyMobile.Views;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TravelAgencyMobile.Views
{
    public partial class ItemsPage : ContentPage
    {
        //ItemsViewModel _viewModel;

        public ItemsPage()
        {
            InitializeComponent();

            this.BindingContext = this;

            DataGenerator.VoucherProcessed += () => { Vouchers = new ObservableCollection<Voucher>(DataGenerator.GetVouchers()); };
            Vouchers = new ObservableCollection<Voucher>(DataGenerator.GetVouchers());
        }


        
        private ObservableCollection<Voucher> _vouchers;

        public ObservableCollection<Voucher> Vouchers
        { 
            get => _vouchers;
            set
            {
                _vouchers = value;
                OnPropertyChanged();
            }
        }

        private Voucher _voucher;

        public Voucher Voucher
        {
            get => _voucher;
            set
            {
                _voucher = value;
                OnPropertyChanged();
            }
        }


        private async void OnDeleteButton_Clicked(object sender, EventArgs e)
        {
            var voucher = ((Button) sender).BindingContext as Voucher;
            bool result = await DisplayAlert("Подтверждение", "Вы действительно хотите удалить заявку?", "Да", "Нет");

            if (result)
            {
                //Vouchers.Remove(Voucher);
                DataGenerator.RemoveVoucher(voucher);
            }
        }
    }
}