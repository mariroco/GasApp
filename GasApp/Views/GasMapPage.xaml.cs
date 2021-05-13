using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Forms.Maps; //INSTALAMOS PAQUETE NUGET Xamarin.Forms.Map
using GasApp.Models;

namespace GasApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GasMapPage : ContentPage
    {
        public GasMapPage(GasModel GasSelected)
        {
            InitializeComponent();

            MapGas.MoveToRegion(
              MapSpan.FromCenterAndRadius(
                  new Position(
                      GasSelected.Latitud,
                      GasSelected.Longitud
                      ),
                  Distance.FromMiles(.5)
               )
          );

            // Agrega un pin al mapa con las coordenadas
            MapGas.Pins.Add(
                new Pin
                {
                    Type = PinType.Place,
                    Label = GasSelected.Marca,
                    Position = new Position(
                        GasSelected.Latitud,
                        GasSelected.Longitud
                        )
                }
                );

            GasMarca.Text = GasSelected.Marca;
            GasSucursal.Text = GasSelected.Sucursal;

        }
    }
}