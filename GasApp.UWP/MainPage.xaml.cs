using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace GasApp.UWP
{
    public sealed partial class MainPage
    {
        public MainPage()
        {
            this.InitializeComponent();

            Xamarin.FormsMaps.Init("AhFQjK_8E_6ejIELDLZwWRw7PUWT6u4VqgNUk4ZRY71xV7sY_M5Kx7c1YgKhuA-J");//< !--agregada para maps-->

            LoadApplication(new GasApp.App());
        }
    }
}
