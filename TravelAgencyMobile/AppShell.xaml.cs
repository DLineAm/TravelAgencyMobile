using System;
using System.Collections.Generic;
using Plugin.SharedTransitions;
using TravelAgency;
using TravelAgencyMobile.Models;
using TravelAgencyMobile.ViewModels;
using TravelAgencyMobile.Views;

using Xamarin.Forms;

namespace TravelAgencyMobile
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(ItemDetailPage), typeof(ItemDetailPage));
            Routing.RegisterRoute(nameof(NewItemPage), typeof(NewItemPage));

            this.BindingContext = this;

            DataGenerator.VoucherProcessed += () =>
            {
                var list = DataGenerator.GetVouchers().Count;
                NewVouchersCount = Math.Abs(OldVouchersCount - list);
                
                Vouchers = DataGenerator.GetVouchers();
                OldVouchersCount = Vouchers.Count;
            };
            Vouchers = DataGenerator.GetVouchers();
            OldVouchersCount = Vouchers.Count;
        }

        private List<Voucher> _vouchers;

        public List<Voucher> Vouchers
        {
            get => _vouchers;
            set
            {
                _vouchers = value;
                OnPropertyChanged();
            }
        }

        private int _oldVouchersCount;

        public int OldVouchersCount
        {
            get => _oldVouchersCount;
            set
            {
                _oldVouchersCount = value;
                OnPropertyChanged();
            }
        }

        private int _newVouchersCount;

        public int NewVouchersCount
        {
            get => _newVouchersCount;
            set
            {
                _newVouchersCount = value;
                OnPropertyChanged();
            }
        }

    }
}
