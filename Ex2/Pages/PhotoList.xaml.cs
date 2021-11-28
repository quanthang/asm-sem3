using Ex2.Entities;
using Ex2.Service;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Media.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Ex2.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class PhotoList : Page
    {
        private PhotoService photoService = new PhotoService();
        public PhotoList()
        {
            this.InitializeComponent();
            this.Loaded += ListPhoto_Loaded;
        }

        private async void ListPhoto_Loaded(object sender, RoutedEventArgs e)
        {
            var listSong = await photoService.GetLatestPhotoAsync();
            MyListView.ItemsSource = listSong;
        }

        private void MyListView_Tapped(object sender, TappedRoutedEventArgs e)
        {
            var selectedItem = (Photo)MyListView.SelectedItem;
        }
    }
}
