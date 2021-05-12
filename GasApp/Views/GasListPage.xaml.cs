using GasApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GasApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GasListPage : ContentPage
    {
        public GasListPage()
        {
            InitializeComponent();
            BindingContext = new GasListViewModel();
        }
    }
}