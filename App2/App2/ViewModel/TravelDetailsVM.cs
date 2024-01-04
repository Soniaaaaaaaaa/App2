using App2.Helpers;
using App2.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Xamarin.Forms;

namespace App2.ViewModel
{
    public class TravelDetailsVM 
    {
        public Command UpdateCommand { get; set; }
        public Command DeleteCommand { get; set; }

        public Post SelectedPost {get; set;}

        public TravelDetailsVM()
        {
            UpdateCommand = new Command<string>(Update, CanUpdate);
            DeleteCommand = new Command(Delete);
        }

        private async void Update(string newExperience)
        {
            SelectedPost.Experience = newExperience;
            var res = await Firestore.Update(SelectedPost);
            if (res) await App.Current.MainPage.Navigation.PopAsync();
        }

        private bool CanUpdate(string newExperience)
        {
            if(string.IsNullOrEmpty(newExperience)) return false;
            return true;
        }
        private async void Delete()
        {
            var res = await Firestore.Delete(SelectedPost);
            if (res) await App.Current.MainPage.Navigation.PopAsync();
        }
    }
}
