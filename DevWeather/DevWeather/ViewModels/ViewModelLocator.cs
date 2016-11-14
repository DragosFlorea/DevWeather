using DevWeather.ViewModels;
using DevWeather.Views;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Views;
using Microsoft.Practices.ServiceLocation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevWeather.ViewModels
{
    public class ViewModelLocator
    {
        public const string FirstPageKey = "FirstPage";
        public const string SecondPageKey = "SecondPage";
        public ViewModelLocator()
        {
            var nav = new NavigationService();
            nav.Configure(FirstPageKey, typeof(MainPage));
            nav.Configure(SecondPageKey, typeof(AddNewLoc_Page));
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);
            if (ViewModelBase.IsInDesignModeStatic)
            { }
            else { }         
            SimpleIoc.Default.Register<ListWeatherData_VM>();
            SimpleIoc.Default.Register<MainListWeater_VM>();
            SimpleIoc.Default.Unregister<INavigationService>();
            SimpleIoc.Default.Register<INavigationService>(() => nav);
          
        }
        public ListWeatherData_VM ListPageInstance
        {
            get { return ServiceLocator.Current.GetInstance<ListWeatherData_VM>(); }
        }
        public MainListWeater_VM MainListPageInstance
        {
            get { return ServiceLocator.Current.GetInstance<MainListWeater_VM>(); }
        }
        public MainPage MainPageInstance
        {
            get { return ServiceLocator.Current.GetInstance<MainPage>(); }
        }
        public static void Cleanup() { }
    }



}
