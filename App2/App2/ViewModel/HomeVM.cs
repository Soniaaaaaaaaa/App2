using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace App2.ViewModel
{
    internal class HomeVM
    {
        public NewTravelCommand NewTravelCommand {  get; set; }
        public HomeVM()
        {
            NewTravelCommand = new NewTravelCommand(this);
        }
        public void NewTravelNavigation()
        {
            App.Current.MainPage.Navigation.PushAsync(new NewTravelPage());

        }

    }

    class NewTravelCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;
        private HomeVM vm;
        public NewTravelCommand(HomeVM VM)
        {
            vm = VM;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            vm.NewTravelNavigation();
        }
    }
}
