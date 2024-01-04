using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using App2.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Firebase.Auth;

[assembly: Dependency(typeof(App2.Droid.Dependencies.Auth))]

namespace App2.Droid.Dependencies
{
    
    public class Auth : IAuth
    {
        public string GetCurrentUserId()
        {
            return FirebaseAuth.Instance.CurrentUser.Uid;
        }
        public bool IsAuthenticated()
        {
            return FirebaseAuth.Instance.CurrentUser != null;
        }

        public async Task< bool> LoginUser(string email, string password)
        {
            try
            {
                await FirebaseAuth.Instance.SignInWithEmailAndPasswordAsync(email, password);
                return true;
            }
            catch (FirebaseAuthWeakPasswordException ex)
            {
                throw new Exception(ex.Message);
            }
            catch (FirebaseAuthInvalidCredentialsException ex)
            {
                throw new Exception(ex.Message);
            }
            catch(FirebaseAuthInvalidUserException ex)
            {
                throw new Exception("There is no such user!");
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<bool> RegisterUser(string email, string password)
        {
            try
            {
                await FirebaseAuth.Instance.CreateUserWithEmailAndPasswordAsync(email, password);
                return true;
            }
            catch (FirebaseAuthWeakPasswordException ex)
            {
                throw new Exception(ex.Message);
            }
            catch (FirebaseAuthInvalidCredentialsException ex)
            {
                throw new Exception(ex.Message);
            }
            catch (FirebaseAuthUserCollisionException ex)
            {
                throw new Exception("There is no such user!");
            }
            catch (Exception e)
            {
                throw new Exception("There was unknown error.");
            }
        }
    }
}