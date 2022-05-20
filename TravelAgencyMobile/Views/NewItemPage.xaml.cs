using System;
using System.Collections.Generic;
using System.ComponentModel;

using TravelAgencyMobile.Models;
using TravelAgencyMobile.ViewModels;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TravelAgencyMobile.Views
{
    public partial class NewItemPage : ContentPage
    {
        public Item Item { get; set; }

        public NewItemPage()
        {
            InitializeComponent();
            BindingContext = new NewItemViewModel();
        }
    }
}