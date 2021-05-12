using GasApp.Models;
using GasApp.Services;
using Plugin.Media;
using GasApp.Data;
using System;
using Xamarin.Forms;

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



        public GasDetailViewModel()
        {
            GasSelected = new GasModel();
        }

        public GasDetailViewModel(GasModel gasSelected)
        {
            GasSelected = gasSelected;
            ImageBase64 = GasSelected.Foto;
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
                    Directory = "AppPets",
                    Name = "PetPicture.jpg"
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
    }
}

