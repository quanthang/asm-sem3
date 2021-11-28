using form.Entities;
using form.Service;
using System;
using Windows.Storage.Pickers;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace form.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class CreateSong : Page
    {
        private FileService fileService;
        private AccountService accountService;
        private SongService songService;
        private static string _accessToken;
        public CreateSong()
        {
            this.InitializeComponent();
            this.Loaded += CreateSong_Loaded; ;
        }

        private async void CreateSong_Loaded(object sender, RoutedEventArgs e)
        {
            fileService = new FileService();
            accountService = new AccountService();
            songService = new SongService();
            var credential = await accountService.LoadAccessTokenFromFile();
            _accessToken = credential.access_token;
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            var song = new Song()
            {
                name = Name.Text,
                author = Author.Text,
                singer = Singer.Text,
                thumbnail = Thumbnail.Text,
                link = Link.Text,
                message = Message.Text
            };
            var result = await songService.CreateSongAsync(song);
            if (result != null)
            {
                ContentDialog contentDialog = new ContentDialog();
                contentDialog.Title = "Action success";
                contentDialog.Content = $"Upload song success.";
                contentDialog.PrimaryButtonText = "Okie";
                await contentDialog.ShowAsync();
            }
            else
            {
                ContentDialog contentDialog = new ContentDialog();
                contentDialog.Title = "Action fails";
                contentDialog.Content = $"Register fails. Please try again!";
                contentDialog.PrimaryButtonText = "Okie";
                await contentDialog.ShowAsync();
            }
        }

        private async void Handle_Click_UploadImages(object sender, RoutedEventArgs e)
        {
            FileOpenPicker picker = new FileOpenPicker
            {
                ViewMode = PickerViewMode.Thumbnail,
                SuggestedStartLocation = PickerLocationId.Downloads
            };
            picker.FileTypeFilter.Add(".jpg");
            picker.FileTypeFilter.Add(".jpeg");
            picker.FileTypeFilter.Add(".png");
            Windows.Storage.StorageFile file = await picker.PickSingleFileAsync();
            if (file != null)
            {
                string result = await fileService.UploadFile(file);
                Thumbnail.Text = result;
            }
        }

        private async void Handle_Click_UploadMp3(object sender, RoutedEventArgs e)
        {
            FileOpenPicker picker = new FileOpenPicker
            {
                ViewMode = PickerViewMode.Thumbnail,
                SuggestedStartLocation = PickerLocationId.Downloads
            };
            picker.FileTypeFilter.Add(".mp3");
            Windows.Storage.StorageFile file = await picker.PickSingleFileAsync();
            if (file != null)
            {
                string result = await fileService.UploadFile(file);
                Link.Text = result;
            }
        }
    }
}
