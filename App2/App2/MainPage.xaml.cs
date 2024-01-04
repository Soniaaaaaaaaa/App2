using App2.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;


namespace App2
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            var assembly = typeof(MainPage);

            iconImage.Source = ImageSource.FromResource("App2.Assets.Images.flower.png", assembly);
        }
    }
}
