using GasApp.Models;
using GasApp.Views;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace GasApp.ViewModels
{
    public class GasListViewModel : BaseViewModel
    {
        private static GasListViewModel instance;

        Command newGasCommand;
        public Command NewGasCommand => newGasCommand ?? (newGasCommand = new Command(NewGasAction));

        List<GasModel> gasolineras;
        public List<GasModel> Gasolineras
        {
            get => gasolineras;
            set => SetProperty(ref gasolineras, value);
        }

        GasModel gasSelected;
        public GasModel GasSelected
        {
            get => gasSelected;
            set
            {
                if (SetProperty(ref gasSelected, value))
                {
                    SelectGasAction();
                }
            }
        }


        public GasListViewModel()
        {
            instance = this;
            LoadGasolineras();
        }

        public static GasListViewModel GetInstance()
        {
            return instance;
        }

        public async void LoadGasolineras()
        {
            Gasolineras = await App.SQLiteDatabase.GetAllGasAsync();
        }

        private void NewGasAction()
        {
            Application.Current.MainPage.Navigation.PushAsync(new GasDetailPage());
        }

        private void SelectGasAction()
        {
            Application.Current.MainPage.Navigation.PushAsync(new GasDetailPage(GasSelected));

        }

    }
}
