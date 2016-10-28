using DevWeather.Models;
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
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;
using static DevWeather.OpenWeatherMapProxy;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409
// 0a08a218caa65034f65bba26714aec11
//https://msdn.microsoft.com/en-us/windows/uwp/maps-and-location/get-location
namespace DevWeather
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    
    public sealed partial class MainPage : Page
    {

        public MainPage()
        {
            this.InitializeComponent();
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                var position = await LocationManager.GetPosition();
                var x = UnitsToggle.IsOn;
                RootObject myweather = await OpenWeatherMapProxy.GetWeather(
                   // position.Coordinate.Latitude,
                   // position.Coordinate.Longitude,
                   "Timisoara",
                    UnitsToggle.IsOn);
                txtDisplayLocation.Text = myweather.name;
                txtDisplayTemp.Text = ((int)myweather.main.temp).ToString();
                txtDisplayDescrp.Text = myweather.weather[0].description;
                string icon = String.Format("ms-appx:///Assets/{0}.png", myweather.weather[0].icon);
                ResultImage.Source = new BitmapImage(new Uri(icon, UriKind.Absolute));
            }
            catch
            {
                txtDisplayLocation.Text = "Unable to get weather!";
            }


        }

        private void AppBarButton_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(AddNewLoc_Page));
        }
    }
}
