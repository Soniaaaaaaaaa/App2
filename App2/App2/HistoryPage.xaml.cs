using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using App2.Model;
using App2.Helpers;
using App2.ViewModel;

namespace App2
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class HistoryPage : ContentPage
	{
		private HistoryVM vm;
		public HistoryPage ()
		{
			InitializeComponent ();
			vm = Resources["vm"] as HistoryVM;
		}
        protected override void OnAppearing()
        {
            base.OnAppearing();
			vm.GetPosts();
        }
    }
}