using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using RAMACHAT.Resources;
using Quobject.SocketIoClientDotNet.Client;
using System.Collections.Generic;
using System.Diagnostics;
using RAMACHAT.SocketIO;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Windows.Media.Imaging;
using RAMACHAT.ThemeChat;

namespace RAMACHAT.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {


        //public static List<string> ThemeChat = new List<string>();
        public List<ThemeChatItem> ThemeChatlist = new List<ThemeChatItem>();
        //public static BitmapImage CurentTheme;
        
        public void getAllThemChat()
        {
            for (int i = 0; i < 3; i++)
            {
                var s = new ThemeChatItem(){ImageSource = ("/ThemeChat/" + (i+1).ToString() + ".jpg")};
                ThemeChatlist.Add(s);
            }
        }
        //public string setThemChat(int i)
        //{
        //    string s;
        //    s = ("/ThemeChat/" + i.ToString() + ".jpg");
        //    //CurentTheme = new BitmapImage(new Uri(s,UriKind.RelativeOrAbsolute));
        //    //CurentTheme = new BitmapImage(new Uri("http://img.v3.news.zdn.vn/w660/Uploaded/Ycgmvlbp/2014_05_29/ly1.jpg", UriKind.RelativeOrAbsolute));
        //    return s;
        //}
        //public void addThemeList()
        //{
        //    for (int i = 0; i < 3; i++)
        //    {
        //        ThemeChat.Add(setThemChat(i));
                
        //    }

        //}






        public MainViewModel()
        {
            this.Items = new ObservableCollection<ItemViewModel>();
            
        }
        
        public void Adddata(string text, string creatAt, string ID)
        {
            this.Items.Add(new ItemViewModel() { MessageText = text, CreateAt = creatAt, SenderID = ID });
        }
        /// <summary>
        /// A collection for ItemViewModel objects.
        /// </summary>
        public ObservableCollection<ItemViewModel> Items { get; set; }

        private string _sampleProperty = "Sample Runtime Property Value";
        /// <summary>
        /// Sample ViewModel property; this property is used in the view to display its value using a Binding
        /// </summary>
        /// <returns></returns>
        public string SampleProperty
        {
            get
            {
                return _sampleProperty;
            }
            set
            {
                if (value != _sampleProperty)
                {
                    _sampleProperty = value;
                    NotifyPropertyChanged("SampleProperty");
                }
            }
        }

        /// <summary>
        /// Sample property that returns a localized string
        /// </summary>
        public string LocalizedSampleProperty
        {
            get
            {
                return AppResources.SampleProperty;
            }
        }

        public bool IsDataLoaded
        {
            get;
            private set;
        }

        /// <summary>
        /// Creates and adds a few ItemViewModel objects into the Items collection.
        /// </summary>
        public void LoadData()
        {

            this.Items.Add(new ItemViewModel() { MessageText = "aaaaaaaaa" });
            this.IsDataLoaded = true;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(String propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (null != handler)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}