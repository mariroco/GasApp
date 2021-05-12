using GasApp.Data;
using GasApp.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GasApp
{
    public partial class App : Application
    {
        private static SQLiteDatabase _SQLiteDatabase;
        public static SQLiteDatabase SQLiteDatabase
        {
            get
            {
                if (_SQLiteDatabase == null) _SQLiteDatabase = new SQLiteDatabase();
                return _SQLiteDatabase;
            }
        }

        public App()
        {
            InitializeComponent();

            var navPage = new NavigationPage(new GasListPage());
            navPage.BarBackgroundColor = (Color)App.Current.Resources["PrimaryDarkGreen"];
            navPage.BarTextColor = Color.White;
            MainPage = navPage;
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
