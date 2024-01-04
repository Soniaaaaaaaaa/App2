using Android.App;
using Android.Content;
using Android.Gms.Tasks;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using App2.Helpers;
using App2.Model;
using Firebase.Firestore;
using Java.Interop;
using Java.Util;
using Kotlin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

[assembly: Xamarin.Forms.Dependency(typeof(App2.Droid.Dependencies.Firestore))]

namespace App2.Droid.Dependencies
{
    public class Firestore : Java.Lang.Object, IFirestore, IOnCompleteListener
    {
        public List<Post> posts;
        bool hasReadPosts = false;
        public Firestore()
        {
            posts = new List<Post>();
        }


        public async Task<bool> Delete(Post post)
        {
            try
            {
                var collection = Firebase.Firestore.FirebaseFirestore.Instance.Collection("posts");

                collection.Document(post.Id).Delete();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool Insert(Post post)
        {
            try
            {
                var postDocument = new Dictionary<string, Java.Lang.Object>()
                {
                    {"experience", post.Experience},
                    {"lat" , post.Lat},
                    {"lon", post.Lon },
                    {"venueName", post.VenueName },
                    {"category", post.Category},
                    {"address", post.Address},
                    {"formatted" , post.FormattedAddress},
                    {"distance" , post.Distance},
                    {"userId", Firebase.Auth.FirebaseAuth.Instance.CurrentUser.Uid }
                };

                var collection = Firebase.Firestore.FirebaseFirestore.Instance.Collection("posts");
                collection.Add(new HashMap(postDocument));
                return true;
            }
            catch(Exception e)
            { 
                return false;
            }

        }

        public void OnComplete(Android.Gms.Tasks.Task task)
        {
            if (task.IsSuccessful)
            {
                var documents = (QuerySnapshot)task.Result;

                posts.Clear();
                foreach (var doc in documents.Documents) 
                {
                    Post newPost = new Post()
                    {
                        Experience = doc.Get("experience").ToString(),
                        Lat = (double)doc.Get("lat"),
                        Lon = (double)doc.Get("lon"),
                        VenueName = ((string?)doc.Get("venueName")),
                        Category = doc.Get("category").ToString(),
                        Address = doc.Get("address").ToString(),
                        FormattedAddress = doc.Get("formatted").ToString(),
                        Distance = (int)doc.Get("distance"),
                        Id = doc.Id
                    };
                    posts.Add(newPost);
                }

            }
            else { posts.Clear(); }
            hasReadPosts = true;
        }

        public async Task<List<Post>> Read()
        {

            try
            {
                hasReadPosts = false;
                var collection = Firebase.Firestore.FirebaseFirestore.Instance.Collection("posts");
                var query = collection.WhereEqualTo("userId", Firebase.Auth.FirebaseAuth.Instance.CurrentUser.Uid);

                query.Get().AddOnCompleteListener(this);

                for (int i = 0; i < 50; i++)
                {
                    await System.Threading.Tasks.Task.Delay(100);
                    if (hasReadPosts) break;
                }

                return posts;
            }
            catch (Exception ex)
            {
                return posts;
            }
        }
        public async Task<bool> Update(Post post)
        {
            try
            {
                var postDocument = new Dictionary<string, Java.Lang.Object>()
                {
                    {"experience", post.Experience},
                    {"lat" , post.Lat},
                    {"lon", post.Lon },
                    {"venueName", post.VenueName },
                    {"category", post.Category},
                    {"address", post.Address},
                    {"formatted" , post.FormattedAddress},
                    {"distance" , post.Distance},
                    {"userId", Firebase.Auth.FirebaseAuth.Instance.CurrentUser.Uid }
                };

                var collection = Firebase.Firestore.FirebaseFirestore.Instance.Collection("posts");
                collection.Document(post.Id).Update(postDocument);
                return true;
            }
            catch (Exception e) { return false; }

        }
    }
}