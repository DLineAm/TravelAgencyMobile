using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelAgency;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TravelAgencyMobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HotelDetailsPage : ContentPage
    {
        public HotelDetailsPage(Hotel hotel)
        {
            InitializeComponent();
            this.BindingContext = this;
            Hotel = hotel;
        }

        private Hotel _hotel;

        public Hotel Hotel
        {
            get => _hotel;
            set
            {
                _hotel = value;
                OnPropertyChanged();
            }
        }

        private async void OnAddVoucherButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new VoucherPage(Hotel));
        }
    }
}