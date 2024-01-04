using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using Microsoft.Maui.Controls.Xaml;
using App2.Model;
using Microsoft.Maui.Controls;
using Microsoft.Maui;

namespace App2
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class HistoryPage : ContentPage
	{
		public HistoryPage ()
		{
			InitializeComponent ();
		}
        protected override void OnAppearing()
        {
            base.OnAppearing();

			using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
			{
				conn.CreateTable<Post>();
				var posts = conn.Table<Post>().ToList();
				postListView.ItemsSource = posts;

			}
        }

        private void postListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
			var selectedPost = postListView.SelectedItem as Post;
			if (selectedPost != null)
			{
				Navigation.PushAsync(new PostDetailPage(selectedPost));	
			}
        }
    }
}