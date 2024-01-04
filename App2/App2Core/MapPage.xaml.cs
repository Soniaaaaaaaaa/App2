using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Maui.Controls.Xaml;
using Microsoft.Maui.Controls;
using Microsoft.Maui;

namespace App2
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MapPage : ContentPage
	{
		public MapPage ()
		{
			InitializeComponent ();
		}
        protected override void OnAppearing()
        {
            base.OnAppearing();

			GetLocation();
        }

        private void GetLocation()
        {
            CheckAndRequestLocationPermission();
        }

        private void CheckAndRequestLocationPermission()
        {
            var status = Permissions.CheckStatusAsync<Permissions.LocationWhenInUse>;
        }
    }
}