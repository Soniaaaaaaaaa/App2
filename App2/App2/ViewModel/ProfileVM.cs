using App2.Helpers;
using App2.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace App2.ViewModel
{
    public class ProfileVM : INotifyPropertyChanged
    {
        public ObservableCollection<CategoryCount> Categories;

        private int postCount;  
        public int PostCount { get 
            {
                return postCount; 
            } 
            set 
            {
                postCount=value;
                OnPropertyChanged("PostCount");
            } 
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public ProfileVM()
        {
            Categories = new ObservableCollection<CategoryCount>();
        }

        public async void GetPosts()
        {
            Categories.Clear();

            var posts = await Firestore.Read();

            PostCount = posts.Count();

            var categories = (from p in posts select p.Category).Distinct().ToList();

            foreach (var category in categories)
            {
                var value = (from post in posts
                             where post.Category == category
                             select post).ToList().Count;

                Categories.Add(new CategoryCount
                {
                    Name = category,
                    Count = value
                });
            }
        }
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public class CategoryCount
        {
            public string Name { get; set; }
            public int  Count { get; set; }
        }
    }
}
