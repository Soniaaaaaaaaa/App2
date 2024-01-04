using App2.Model;
using SQLite;
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
	public partial class NewTravelPage : ContentPage
	{
		public NewTravelPage ()
		{
			InitializeComponent ();
		}

        private void ToolbarItem_Clicked(object sender, EventArgs e)
        {
			Post post = new Post()
			{
				Experience = experienceEntry.Text
			};
			using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
			{
				conn.CreateTable<Post>();
				int rows = conn.Insert(post);

                if (rows > 0) { DisplayAlert("Success", "Experience was inserted", "Ok"); }
                else DisplayAlert("Failure", "Experience was not inserted", "Ok");
            }
			
        }
    }
}