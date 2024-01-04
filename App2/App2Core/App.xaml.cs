using System;
using Microsoft.Maui.Controls.Xaml;
using Microsoft.Maui.Controls;
using Microsoft.Maui;

namespace App2
{
    public partial class App : Application
    {
        public static string DatabaseLocation=string.Empty;
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new MainPage());
        }
        public App(string databaseLocation)
        {
            InitializeComponent();

            MainPage = new NavigationPage(new MainPage());

            DatabaseLocation=databaseLocation;
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
