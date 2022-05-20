using DLToolkit.Forms.Controls;

using System;
using System.IO;
using MongoDB.Driver;
using Plugin.SharedTransitions;
using TravelAgencyMobile.Models;
using TravelAgencyMobile.Services;
using TravelAgencyMobile.Views;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Color = Android.Graphics.Color;

namespace TravelAgencyMobile
{
    public partial class App : Application
    {
        public static string FileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "vouchers.json");
        //public static MongoClientSettings Settings { get; } = MongoClientSettings.FromConnectionString("mongodb+srv://user:123@bukakich.z1nzm.mongodb.net/Bukakich?retryWrites=true&w=majority");

        public App()
        {
            InitializeComponent();
           
            FlowListView.Init();
            DependencyService.Register<MockDataStore>();
            MainPage = new AppShell();

            //DataGenerator.ClearJSON();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
            DataGenerator.SerializeVouchers();
        }

        protected override void OnResume()
        {
        }
    }
}
