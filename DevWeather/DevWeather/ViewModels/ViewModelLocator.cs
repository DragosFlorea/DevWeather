using DevWeather.ViewModels;
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
        public ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);
            if (ViewModelBase.IsInDesignModeStatic)
            { }
            else { }
            SimpleIoc.Default.Register<INavigationService, NavigationService>();
            SimpleIoc.Default.Register<ListWeatherData_VM>();
        }

        public ListWeatherData_VM ListPageInstance
        {
            get { return ServiceLocator.Current.GetInstance<ListWeatherData_VM>(); }
        }
        public static void Cleanup() { }
    }



}
