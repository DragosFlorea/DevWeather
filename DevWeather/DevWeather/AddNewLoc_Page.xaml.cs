﻿using DevWeather.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238
//https://mallibone.com/post/hello-universal-windows-platform-uwpwindows-10
namespace DevWeather
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class AddNewLoc_Page : Page
    {
        ViewModelLocator x = new ViewModelLocator();
        public AddNewLoc_Page()
        {
            this.InitializeComponent(); 

        }
        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            
          await x.ListPageInstance.add("Madrid", "metric");
        }

        private void UnitsToggle_Toggled(object sender, RoutedEventArgs e)
        {
            
            x.ListPageInstance.getToggleStatus(UnitsToggle.IsOn);
        }
    }
}
