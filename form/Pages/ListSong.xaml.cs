using form.Entities;
using form.Service;
using System;
using Windows.Media.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace form.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ListSong : Page
    {
        private SongService songService = new SongService();
        public ListSong()
        {
            this.InitializeComponent();
            this.Loaded += ListSong_Loaded;
        }

        private async void ListSong_Loaded(object sender, RoutedEventArgs e)
        {
            var listSong = await songService.GetLatestSongAsync();
            MyListView.ItemsSource = listSong;
        }

        private void MyListView_Tapped(object sender, TappedRoutedEventArgs e)
        {
            var selectedItem = (Song) MyListView.SelectedItem;
            MyMediaPlayer.Source = MediaSource.CreateFromUri(new Uri(selectedItem.link));
        }
    }
}
