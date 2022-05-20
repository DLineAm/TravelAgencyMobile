using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using TravelAgency;
using TravelAgencyMobile.Models;

using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;

namespace TravelAgencyMobile.Views
{
    public partial class AboutPage : ContentPage
    {
        public AboutPage()
        {
            InitializeComponent();
            this.BindingContext = this;

            DataGenerator.VoucherProcessed += GetVouchers;

            GetVouchers();

            var hotelsCount = DataGenerator.GenerateHotels().Count;
            var usersCount = new Random().Next(10000, 50000);
            Device.StartTimer(TimeSpan.FromMilliseconds(100), () => ProduceHotelsCountCalculating(hotelsCount > 100 ? hotelsCount - 100 : hotelsCount, hotelsCount));
            Device.StartTimer(TimeSpan.FromMilliseconds(100), () => ProduceUsersCountCalculating(usersCount));
        }

        private StringBuilder _usersCountMask = new StringBuilder();
        private StringBuilder _hotelsCountMask = new StringBuilder();

        private void GetVouchers()
        {
            Vouchers = DataGenerator.GetVouchers();
            ProcessedVouchersCount = Vouchers.Count(v => v.BookingStatus == "Не оплачено").ToString();
            ApprovedVouchersCount = Vouchers.Count(v => v.BookingStatus == "Оплачено").ToString();
        }

        private bool ProduceUsersCountCalculating(int maxvalue)
        {
            var valueString = maxvalue.ToString();
            var countMask = new StringBuilder();

            if (_usersCountMask.Length != valueString.Length)
            {
                foreach (var ch in maxvalue.ToString())
                {
                    _usersCountMask.AppendFormat("0");
                }
            }

            var mask = _usersCountMask.ToString().Select((ch, index) => (ch, index));

            foreach (var pair in mask)
            {
                if (valueString[pair.index] > _usersCountMask[pair.index])
                {

                    if (new Random().Next(100) <= 50)
                    {
                        _usersCountMask[pair.index] = Convert.ToChar(Convert.ToInt32(_usersCountMask[pair.index]) + 1);
                        UsersCount = _usersCountMask.ToString();
                    }

                    if (pair.index + 1 == _usersCountMask.Length) break;
                }

            }

            return UsersCount != valueString;
        }

        private bool ProduceHotelsCountCalculating(int value, int maxvalue)
        {
            if (maxvalue < 20)
            {
                var intCount = Convert.ToInt32(HotelsCount);
                if (intCount < value && value != maxvalue)
                {
                    HotelsCount = value.ToString();
                }

                intCount++;
                HotelsCount = intCount.ToString();
                return intCount < maxvalue;
            }
            else
            {
                var valueString = maxvalue.ToString();
                if (_hotelsCountMask.Length != valueString.Length)
                {
                    foreach (var ch in maxvalue.ToString())
                    {
                        _hotelsCountMask.AppendFormat("0");
                    }
                }

                var mask = _hotelsCountMask.ToString().Select((ch, index) => (ch, index));

                foreach (var pair in mask)
                {
                    if (valueString[pair.index] > _hotelsCountMask[pair.index])
                    {

                        if (new Random().Next(100) <= 50)
                        {
                            _hotelsCountMask[pair.index] = Convert.ToChar(Convert.ToInt32(_hotelsCountMask[pair.index]) + 1);
                            HotelsCount = _hotelsCountMask.ToString();
                        }

                        if (pair.index + 1 == _hotelsCountMask.Length) break;
                    }

                }

                return HotelsCount != valueString;
            }
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

        private string _approvedVouchersCount = "0";

        public string ApprovedVouchersCount { get => _approvedVouchersCount; set { _approvedVouchersCount = value; OnPropertyChanged(); } }

        private string _processedVouchersCount = "0";

        public string ProcessedVouchersCount { get => _processedVouchersCount; set { _processedVouchersCount = value; OnPropertyChanged(); } }

        private string _hotelsCount;

        public string HotelsCount { get => _hotelsCount; set { _hotelsCount = value; OnPropertyChanged(); } }

        private string _usersCount;

        public string UsersCount { get => _usersCount; set { _usersCount = value; OnPropertyChanged(); } }
    }
}