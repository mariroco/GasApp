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

        string marca;
        public string Marca
        {
            get => marca;
            set => SetProperty(ref marca, value);
        }

        string sucursal;
        public string Sucursal
        {
            get => sucursal;
            set => SetProperty(ref sucursal, value);
        }

        string foto;
        public string Foto
        {
            get => foto;
            set => SetProperty(ref foto, value);
        }

        double roja;
        public double Roja
        {
            get => roja;
            set => SetProperty(ref roja, value);
        }

        double verde;
        public double Verde
        {
            get => verde;
            set => SetProperty(ref verde, value);
        }


        double diesel;
        public double Diesel
        {
            get => diesel;
            set => SetProperty(ref diesel, value);
        }

        double longitud;
        public double Longitud
        {
            get => longitud;
            set => SetProperty(ref longitud, value);
        }

        double latitud;
        public double Latitud
        {
            get => latitud;
            set => SetProperty(ref latitud, value);
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
