using DLToolkit.Forms.Controls;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Android.Widget;
using TravelAgency;

using TravelAgencyMobile.Models;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TravelAgencyMobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HotelsPage : ContentPage
    {
        public HotelsPage()
        {
            InitializeComponent();
            this.BindingContext = this;

            var countries = AllHotels.Select(h => h.Country).Distinct().ToList();
            countries.Insert(0, "Все");
            Countries = countries;

            var cities = AllHotels.Select(h => h.City).Distinct().ToList();
            cities.Insert(0, "Все");
            Cities = cities;

            AnimationView.OnFinishedAnimation += (s, e) => { AnimationView.Speed *= -1; };

            GetHotels();
        }

        private bool _orderByDescending;
        public bool IsOrderByDescending { get => _orderByDescending; set { _orderByDescending = value; OnPropertyChanged(); } }

        private string _searchData;
        public string SearchData
        {
            get => _searchData; set
            {

                _searchData = value;
                OnPropertyChanged();
                SearchHotel();
            }
        }

        private void SearchHotel()
        {
            if (String.IsNullOrWhiteSpace(SearchData))
            {
                Hotels = AllHotels;
                return;
            }
            Hotels = AllHotels;
            Hotels = Hotels.Where(h => h.Name.ToLower().Contains(SearchData.ToLower()) ||
                                       h.Address.ToLower().Contains(SearchData.ToLower()) ||
                                       h.City.ToLower().Contains(SearchData.ToLower()) ||
                                       h.Country.ToLower().Contains(SearchData.ToLower())).ToList();
        }

        private void FilterByCountry()
        {
            Hotels = AllHotels;
            SearchHotel();
            if (string.IsNullOrWhiteSpace(CountryFilter) || CountryFilter == "Все") return;
            Hotels = Hotels.Where(h => h.Country == CountryFilter).ToList();
        }

        private List<string> _countries;
        public List<string> Countries
        {
            get => _countries; set
            {
                _countries = value;
                OnPropertyChanged();
            }
        }

        private string _countryFilter;
        public string CountryFilter
        {
            get => _countryFilter; set
            {
                _countryFilter = value;
                OnPropertyChanged();
                FilterByCountry();
                CityFilter = "Все";
            }
        }

        private List<string> _cities;
        public List<string> Cities
        {
            get => _cities; set
            {
                _cities = value;
                OnPropertyChanged();
            }
        }

        private string _cityFilter;

        public string CityFilter
        {
            get => _cityFilter;
            set
            {
                _cityFilter = value;
                OnPropertyChanged();
                FilterByCountry();
                FilterByCity();
            }
        }

        private void FilterByCity()
        {
            if (CityFilter == "Все" || CityFilter == null)
            {
                return;
            }
            Hotels = Hotels.Where(h => h.City == CityFilter).ToList();
        }

        private List<Hotel> _hotels;
        public List<Hotel> Hotels
        {
            get => _hotels ?? (_hotels = AllHotels); set
            {
                _hotels = value;
                OnPropertyChanged();
            }
        }

        private Hotel _hotel;
        public Hotel Hotel
        {
            get => _hotel; set
            {
                _hotel = value;
                OnPropertyChanged();
            }
        }

        private List<Hotel> _allHotels;
        public List<Hotel> AllHotels
        {
            get => _allHotels ?? (_allHotels = DataGenerator.GenerateHotels()); set
            {
                _allHotels = value;
                OnPropertyChanged();
            }
        }

        //public ICommand ItemTappedCommand => new Command<Hotel>(async h =>
        //{
        //    Hotel = h;
        //    var page = new HotelDetailsPage(h);
        //    await Navigation.PushAsync(page);
        //});

        private async void TapGestureRecognizer_OnTapped(object sender, EventArgs e)
        {
            var selectedHotel = (Hotel)((TappedEventArgs)e).Parameter;
            Hotel = selectedHotel;
            var page = new HotelDetailsPage(selectedHotel);
            await Navigation.PushAsync(page);
        }

        private void OnSortButtonClicked(object sender, EventArgs e)
        {
            AnimationView.PlayAnimation();

            IsOrderByDescending = !IsOrderByDescending;

            Order();
        }

        private void Order()
        {
            Hotels = IsOrderByDescending
                ? Hotels.OrderByDescending(h => h.Name).ToList()
                : Hotels.OrderBy(h => h.Name).ToList();
        }

        private void GetHotels()
        {
            Hotels = AllHotels;
            FilterByCountry();
            FilterByCity();
            Order();
        }
    }
}