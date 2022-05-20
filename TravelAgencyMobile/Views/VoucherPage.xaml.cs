using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelAgency;
using TravelAgencyMobile.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TravelAgencyMobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class VoucherPage : ContentPage
    {
        public VoucherPage(Hotel hotel)
        {
            InitializeComponent();
            this.BindingContext = this;
            Hotel = hotel;
            RestTypes = DataGenerator.GenerateRestTypes();
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

        private DateTime _dateStart = DateTime.Now;
        public DateTime DateStart
        {
            get => _dateStart;
            set
            {
                _dateStart = value;
                OnPropertyChanged();
            }
        }

        private DateTime _dateFinish = DateTime.Now;
        public DateTime DateFinish
        {
            get => _dateFinish;
            set
            {
                _dateFinish = value;
                OnPropertyChanged();
            }
        }

        private List<RestType> _restTypes;
        public List<RestType> RestTypes
        {
            get => _restTypes;
            set
            {
                _restTypes = value;
                OnPropertyChanged();
            }
        }

        private RestType _restType;
        public RestType RestType
        {
            get => _restType;
            set
            {
                _restType = value;
                OnPropertyChanged();
            }
        }

        private async void Button_OnClicked(object sender, EventArgs e)
        {
            var errorText = string.Empty;
            if ((DateFinish - DateStart).Days <= 0)
            {
                errorText += "Выберите правильную дату вылета и прилета. ";
            }

            if (RestType == null)
            {
                errorText += "Выберите вид отдыха. ";
            }

            if (errorText != string.Empty)
            {
                await DisplayAlert("Ошибка", errorText, "ok");
                return;
            }

            var voucher = new Voucher { StartDate = DateStart, EndDate = DateFinish, RestType = RestType, BookingStatus = "Не оплачено", Hotel = Hotel};

            DataGenerator.AddVoucher(voucher);

            AnimationView.PlayAnimation();
            await SuccessStackLayout.FadeTo(0, 300);
            await SuccessTextLayout.FadeTo(1, 400);
            

            //await DisplayAlert("Уведомление",
            //    "Заявка была создана, список заявок можно посмотреть во вкладдке «Заявки» ", "ok");
            //await Navigation.PopToRootAsync(true);
        }

        private async void OnBackButton_CLicked(object sender, EventArgs e)
        {
            await Navigation.PopToRootAsync(true);
        }
    }
}