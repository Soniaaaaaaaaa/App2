using App2.Helpers;
using App2.Model;
using Plugin.Geolocator;
using Plugin.Geolocator.Abstractions;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.GoogleMaps;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Xaml;

namespace App2
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MapPage : ContentPage
	{
        IGeolocator locator = CrossGeolocator.Current;
        public MapPage ()
		{
			InitializeComponent ();
		}
        protected override void OnAppearing()
        {
            base.OnAppearing();

			GetLocation();
            GetPosts();
        }

        private async void GetPosts()
        {

             DisplayOnMap(await Firestore.Read());
        }

        private void DisplayOnMap(List<Post> posts)
        {
            foreach (var post in posts)
            {
                try
                {
                    var pinCoordinates = new Xamarin.Forms.Maps.Position(post.Lat, post.Lon);
                    var pin = new Xamarin.Forms.Maps.Pin
                    {
                        Position = pinCoordinates,
                        Label = post.VenueName,
                        Address = post.Address,
                        Type = Xamarin.Forms.Maps.PinType.SavedPin
                    };
                    locationMap.Pins.Add(pin);
                }
                catch(NullReferenceException nre){}
                catch(Exception ex) { }
            }
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            locator.StopListeningAsync();
        }

        private async void GetLocation()
        {
            var status = await CheckAndRequestLocationPermission();
            if (status == PermissionStatus.Granted)
            {
                var location = await Geolocation.GetLocationAsync();
               
                locator.PositionChanged += Locator_Position_Changed;
                await locator.StartListeningAsync(new TimeSpan(0,1,0),100);
                locationMap.IsShowingUser = true;
                CenterMap(location.Latitude, location.Longitude);
            }
        }

        private void Locator_Position_Changed(object sender, Plugin.Geolocator.Abstractions.PositionEventArgs e)
        {
            CenterMap(e.Position.Latitude, e.Position.Longitude);
        }

        private void CenterMap(double latitude, double longitude)
        {
           Xamarin.Forms.Maps.Position center = new Xamarin.Forms.Maps.Position(latitude, longitude);
           Xamarin.Forms.Maps.MapSpan span = new Xamarin.Forms.Maps.MapSpan(center, 1,1);
           locationMap.MoveToRegion(span);
        }

        private async Task<PermissionStatus> CheckAndRequestLocationPermission()
        {
            var status = await Permissions.CheckStatusAsync<Permissions.LocationWhenInUse>();
            if(status == PermissionStatus.Granted)
            {
                return status;
            }
            if(status == PermissionStatus.Denied && DeviceInfo.Platform==DevicePlatform.iOS)
            {
                return status;
            }
            status = await Permissions.RequestAsync<Permissions.LocationWhenInUse>();
            return status;
        }
    }
}