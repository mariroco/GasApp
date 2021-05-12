using GasApp.Models;
using GasApp.Services;
using Plugin.Media;
using GasApp.Data;
using System;
using Xamarin.Forms;
using Xamarin.Essentials;

namespace GasApp.ViewModels
{
    public class GasDetailViewModel : BaseViewModel
    {
     
        Command takePictureCommand;
        public Command TakePictureCommand => takePictureCommand ?? (takePictureCommand = new Command(TakePictureAction));

        Command selectPictureCommand;
        public Command SelectPictureCommand => selectPictureCommand ?? (selectPictureCommand = new Command(SelectPictureAction));

        Command saveCommand;
        public Command SaveCommand => saveCommand ?? (saveCommand = new Command(SaveAction));

        Command deleteCommand;
        public Command DeleteCommand => deleteCommand ?? (deleteCommand = new Command(DeleteAction));

        Command cancelCommand;
        public Command CancelCommand => cancelCommand ?? (cancelCommand = new Command(CancelAction));

        Command getlocationCommand;
        public Command GetLocationCommand => getlocationCommand ?? (getlocationCommand = new Command(GetLocationAction));

        GasModel gasSelected;
        public GasModel GasSelected
        {
            get => gasSelected;
            set => SetProperty(ref gasSelected, value);
        }

        string imageBase64;
        public string ImageBase64
        {
            get => imageBase64;
            set => SetProperty(ref imageBase64, value);
        }

        double latitud;
        public double gasLatitud
        {
            get => latitud;
            set => SetProperty(ref latitud, value);
        }

        double longitud;
        public double gasLongitud
        {
            get => longitud;
            set => SetProperty(ref longitud, value);
        }

        public GasDetailViewModel()
        {
            GasSelected = new GasModel();
        }

        public GasDetailViewModel(GasModel gas)
        {
            GasSelected = gas;
            ImageBase64 = GasSelected.Foto;
            gasLatitud = GasSelected.Latitud;
            gasLongitud = gasSelected.Longitud;
           
        }

        private async void TakePictureAction()
        {
            try
            {
                await CrossMedia.Current.Initialize();

                if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
                {
                    await Application.Current.MainPage.DisplayAlert("Gasolineras", "No existe cámara disponible en el dispositivo", "Ok");
                    return;
                }

                var file = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
                {
                    Directory = "GasApp",
                    Name = "GasFoto.jpg"
                });

                if (file == null)
                    return;
                GasSelected.Foto = ImageBase64 = await new ImageService().ConvertImageFilePathToBase64(file.Path);

            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Gasolineras", $"Se generó un error al tomar la fotografía ({ex.Message})", "Ok");
            }
        }

        private async void SelectPictureAction()
        {
            try
            {
                await CrossMedia.Current.Initialize();

                if (!CrossMedia.Current.IsPickPhotoSupported)
                {
                    await Application.Current.MainPage.DisplayAlert("Gasolineras", "No es posible seleccionar fotografías en el dispositivo", "Ok");
                    return;
                }

                var file = await CrossMedia.Current.PickPhotoAsync(new Plugin.Media.Abstractions.PickMediaOptions
                {
                    PhotoSize = Plugin.Media.Abstractions.PhotoSize.Medium
                });

                if (file == null)
                    return;

                GasSelected.Foto = ImageBase64 = await new ImageService().ConvertImageFilePathToBase64(file.Path);

            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Gasolineras", $"Se generó un error al tomar la fotografía ({ex.Message})", "Ok");
            }
        }

        private async void SaveAction(object obj)
        {
            await App.SQLiteDatabase.SaveGasAsync(GasSelected);
 
            GasListViewModel.GetInstance().LoadGasolineras();
            await Application.Current.MainPage.Navigation.PopAsync();

        }
        private async void DeleteAction(object obj)
        {
            await App.SQLiteDatabase.DeleteGasAsync(GasSelected);
            GasListViewModel.GetInstance().LoadGasolineras();
            await Application.Current.MainPage.Navigation.PopAsync();

        }
        private async void CancelAction(object obj)
        {
            await Application.Current.MainPage.Navigation.PopAsync();

        }

        public async void GetLocationAction()
        {
            try
            {
                GasSelected.Longitud = GasSelected.Latitud = 0;

                var location = await Geolocation.GetLastKnownLocationAsync();

                if (location != null)
                {
                    GasSelected.Latitud = gasLatitud = location.Latitude;
                    GasSelected.Longitud = gasLongitud = location.Longitude;
                   
                }
            }
            catch (FeatureNotSupportedException fnsEx)
            {
                // Handle not supported on device exception
                await Application.Current.MainPage.DisplayAlert("AppPets", $"El GPS no esta soportado en el dispositivo ({fnsEx.Message})", "Ok");
            }
            catch (FeatureNotEnabledException fneEx)
            {
                // Handle not enabled on device exception
                await Application.Current.MainPage.DisplayAlert("Gasolineras", $"El GPS no esta activado en el dispositivo ({fneEx.Message})", "Ok");
            }
            catch (PermissionException pEx)
            {
                // Handle permission exception
                await Application.Current.MainPage.DisplayAlert("Gasolineras", $"No se pudo obtener el permiso para las coordenadas ({pEx.Message})", "Ok");
            }
            catch (Exception ex)
            {
                // Unable to get location
                await Application.Current.MainPage.DisplayAlert("Gasolineras", $"Se genero un error al obtener las coordenadas del dispositivo ({ex.Message})", "Ok");
            }
        }
    }
}

