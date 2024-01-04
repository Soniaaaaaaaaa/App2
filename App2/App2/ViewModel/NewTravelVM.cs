using App2.Helpers;
using App2.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace App2.ViewModel
{
    public class NewTravelVM : INotifyPropertyChanged
    {
        public ObservableCollection<Feature> Venues { get; set; }

        public Command SaveCommand { get; set; }

        private string experience;
        public string Experience
        {
            get {  return experience; }

            set 
            {
                experience = value;
                OnProrertyChanged("Experience");
                OnProrertyChanged("PostIsReady");
            }
        }
        private Feature selectedVenue;
        public Feature SelectedVenue { 
            get { return selectedVenue; } 
            set { 
                selectedVenue = value;
                OnProrertyChanged("PostIsReady");
            }
        }
        private bool postIsReady;
        public bool PostIsReady { get { return !string.IsNullOrEmpty(Experience) && SelectedVenue != null; } }
        public event PropertyChangedEventHandler PropertyChanged;

        public NewTravelVM()
        {
            Venues = new ObservableCollection<Feature>();
            SaveCommand = new Command<bool>(Save, CanSave);
        }
        private bool CanSave(bool param)
        {
            return param;
        }
        private async void Save(bool param)
        {
            try
            {

                Post post = new Post()
                {
                    Experience = Experience,
                    VenueName = SelectedVenue.properties.name,
                    Category = SelectedVenue.properties.categories.FirstOrDefault(),
                    FormattedAddress = SelectedVenue.properties.formatted,
                    Distance = SelectedVenue.properties.distance,
                    Lon = SelectedVenue.properties.lon,
                    Lat = SelectedVenue.properties.lat,
                    Address = SelectedVenue.properties.address_line1


                };

                bool res = Firestore.Insert(post);
                if (res) { await App.Current.MainPage.DisplayAlert("Success", "Experience was saved", "Ok"); }
                else await App.Current.MainPage.DisplayAlert("Failure", "Experience was not saved", "Ok");
                await App.Current.MainPage.Navigation.PopAsync();
            }
            catch (Exception ex)
            {

            }
        }

        public async void GetVenue(double lat, double lon)
        {
            var venues = await Feature.GetVenues(lat, lon);

            Venues.Clear();
            foreach(var venue in venues) 
            { 
                Venues.Add(venue);
            }
            
        }
        private void OnProrertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

}
