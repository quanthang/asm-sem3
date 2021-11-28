using form.Service;
using form.Entities;
using System;
using Windows.Foundation;
using Windows.Media.Capture;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace form.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class RegisterPage : Page
    {
        private AccountService accountService = new AccountService();
        private FileService fileService = new FileService();
        private int choosedGender = 1;
        private string _avatarUrl;

        public RegisterPage()
        {
            this.InitializeComponent();
            ApplicationView.PreferredLaunchViewSize = new Size(480, 400);
            ApplicationView.PreferredLaunchWindowingMode = ApplicationViewWindowingMode.PreferredLaunchViewSize;
        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            RadioButton radioButton = sender as RadioButton;
            var currentTag = radioButton.Tag;
            switch (currentTag)
            {
                case "Male":
                    choosedGender = 1;
                    break;
                case "Female":
                    choosedGender = 0;
                    break;
                case "Other":
                    choosedGender = 2;
                    break;
            }
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            var account = new Account()
            {
                firstName = FirstName.Text,
                lastName = LastName.Text,
                password = Password.Password.ToString(),
                avatar = Avatar.Text,
                email = Email.Text,
                phone = Phone.Text,
                address = Address.Text,
                introduction = Introduction.Text,
                gender = choosedGender,
                birthday = datePicker.SelectedDate.Value.ToString("yyyy-MM-dd"),
            };
            var result = await accountService.RegisterAsync(account);
            if (result)
            {
                ContentDialog contentDialog = new ContentDialog();
                contentDialog.Title = "Action success";
                contentDialog.Content = $"Register success. Your email is {account.email}";
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
                this.Frame.Navigate(typeof(Pages.LoginPage));
            }
        }

        private void HyperlinkButton_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(Pages.LoginPage));
        }

        private async void Handle_Camera(object sender, RoutedEventArgs e)
        {
            CameraCaptureUI cameraUI = new CameraCaptureUI();
            cameraUI.PhotoSettings.Format = CameraCaptureUIPhotoFormat.Jpeg;
            cameraUI.PhotoSettings.CroppedSizeInPixels = new Size(200, 200);
            StorageFile photo = await cameraUI.CaptureFileAsync(CameraCaptureUIMode.Photo);
            if (photo != null)
            {
                ProgressRing.IsActive = true;
                var result = await fileService.UploadFile(photo);
                if (result != null)
                {
                    _avatarUrl = result;
                    PreviewPhoto.ImageSource = new BitmapImage(new Uri(result));
                    PreviewPhoto.Stretch = Windows.UI.Xaml.Media.Stretch.UniformToFill;
                }
                ProgressRing.IsActive = false;
            }
        }

        private async void Handle_Upload_Image(object sender, RoutedEventArgs e)
        {
            FileOpenPicker filePicker = new FileOpenPicker();
            filePicker.SuggestedStartLocation = PickerLocationId.PicturesLibrary;
            filePicker.FileTypeFilter.Add(".jpg");
            filePicker.FileTypeFilter.Add(".jpeg");
            filePicker.FileTypeFilter.Add(".png");
            filePicker.ViewMode = PickerViewMode.Thumbnail;
            StorageFile choseFile = await filePicker.PickSingleFileAsync();
            if (choseFile != null)
            {
                ProgressRing.IsActive = true;
                var result = await fileService.UploadFile(choseFile);
                _avatarUrl = result;
                PreviewPhoto.ImageSource = new BitmapImage(new Uri(result));
                PreviewPhoto.Stretch = Windows.UI.Xaml.Media.Stretch.UniformToFill;
            }
            ProgressRing.IsActive = false;
        }
    }
}