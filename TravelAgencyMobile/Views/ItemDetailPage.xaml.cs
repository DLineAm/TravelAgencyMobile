using System.ComponentModel;

using TravelAgencyMobile.ViewModels;

using Xamarin.Forms;

namespace TravelAgencyMobile.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
    }
}