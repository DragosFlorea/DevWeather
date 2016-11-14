using DevWeather.Models;
using DevWeather.ViewModels;
using Microsoft.Practices.ServiceLocation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;
using static DevWeather.OpenWeatherMapProxy;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409
// 0a08a218caa65034f65bba26714aec11
//https://msdn.microsoft.com/en-us/windows/uwp/maps-and-location/get-location
namespace DevWeather.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    
    public sealed partial class MainPage : Page
    {
        private MainListWeater_VM MainPageInstance;
        public MainPage()
        {
            this.InitializeComponent();
        }                                                                                                                                     
        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            this.MainPageInstance = ServiceLocator.Current.GetInstance<MainListWeater_VM>();
            await   MainPageInstance.init();
        }

        private void AppBarButton_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(AddNewLoc_Page));
        }
    }
}
