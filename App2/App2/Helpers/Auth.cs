using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace App2.Helpers
{
    public interface IAuth
    {
        Task<bool> RegisterUser(string email, string password);
        Task<bool> LoginUser(string email, string password);
        bool IsAuthenticated();
        string GetCurrentUserId();
    }
    public class Auth
    {
        public static IAuth auth = DependencyService.Get<IAuth>();
        
        public Auth()
        {

        }
        public static async Task<bool> RegisterUser(string email, string password)
        {
            try { return await auth.RegisterUser(email, password); }
            catch (Exception e)
            {
                await App.Current.MainPage.DisplayAlert("Error!!!", e.Message, "Ok!");
                return false;
            }

        }
        public static async Task<bool> LoginUser(string email, string password)
        {
            try 
            { 
                return await auth.LoginUser(email, password);
            }
            catch (Exception e) 
            {
                await App.Current.MainPage.DisplayAlert("Error!!!", e.Message, "Ok!");
                string registerMessage = "An internal error has occurred. [ INVALID_LOGIN_CREDENTIALS ]";
                if(e.Message.Contains(registerMessage))
                {
                    var repl = await App.Current.MainPage.DisplayPromptAsync("Message", "There is no such user, but we can create you an account", "Ok!", "No, Thanks");
                    if(repl=="Ok!") return await RegisterUser(email, password);  
                } 
                return false;
            }

        }
        public static bool IsAuthenticated()
        {
            return auth.IsAuthenticated();
        }
        public static string GetCurrentUserId()
        {
            return auth.GetCurrentUserId();
        }
    }
}
