using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;
using Microsoft.Maui;

namespace App2
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void loginButton_Clicked(object sender, EventArgs e)
        {
            if( 
                string.IsNullOrEmpty(emailEntry.Text) ||
                string.IsNullOrEmpty(passwordEntry.Text)
                )
            {
                
            }
            else
            {
                Navigation.PushAsync(new HomePage());
            }
        }
    }
}
