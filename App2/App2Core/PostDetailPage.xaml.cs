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
	public partial class PostDetailPage : ContentPage
	{
		Post selectedPost;
		public PostDetailPage (Post selectedPost)
		{
			InitializeComponent ();
			this.selectedPost = selectedPost;
			experienceEntry.Text = selectedPost.Experience;
		}
		void updateButton_Clicked(object sender, EventArgs e)
		{
			selectedPost.Experience = experienceEntry.Text;
            using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
            {
                conn.CreateTable<Post>();
                int rows = conn.Update(selectedPost.Id);

                if (rows > 0) { DisplayAlert("Success", "Experience was updated", "Ok"); }
                else DisplayAlert("Failure", "Experience was not updated", "Ok");
            }
        }

		void deleteButton_Clicked(object sender, EventArgs e)
        {
            using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
            {
                conn.CreateTable<Post>();
                int rows = conn.Delete(selectedPost.Id);

                if (rows > 0) { DisplayAlert("Success", "Experience was deleted", "Ok"); }
                else DisplayAlert("Failure", "Experience was not deleted", "Ok");
            }
        }

       
    }
}