using App2.Helpers;
using App2.Model;
using App2.ViewModel;
using GoogleApi.Entities.Maps.DistanceMatrix.Response;
using Plugin.Geolocator;
using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App2
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class NewTravelPage : ContentPage
	{
		private NewTravelVM vm;
		public NewTravelPage ()
		{
			InitializeComponent ();
			vm = Resources["vm"] as NewTravelVM;
			
		}

        protected override async void OnAppearing()
		{
			base.OnAppearing();

			var locator = CrossGeolocator.Current;

			var position = await locator.GetPositionAsync();
			vm.GetVenue(position.Latitude, position.Longitude);



        }	
    }
} 