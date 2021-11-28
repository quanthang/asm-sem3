using form.Service;
using form.Entities;
using System;
using Windows.Foundation;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace form.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class LoginPage : Page
    {
        private AccountService accountService = new AccountService();
        public LoginPage()
        {
            this.InitializeComponent();
            ApplicationView.PreferredLaunchViewSize = new Size(480, 400);
            ApplicationView.PreferredLaunchWindowingMode = ApplicationViewWindowingMode.PreferredLaunchViewSize;
        }

        private void HyperlinkButton_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(Pages.RegisterPage));
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            var loginInformation = new LoginInformation
            {
                email = txtEmail.Text,
                password = txtPassword.Password.ToString()
            };
            var credential = await accountService.LoginAsync(loginInformation);
            if (credential == null)
            {
                ContentDialog contentDialog = new ContentDialog();
                contentDialog.Title = "Action fails";
                contentDialog.Content = $"Please try again later!";
                contentDialog.PrimaryButtonText = "Got it";
                await contentDialog.ShowAsync();
            }
            else
            {
                ContentDialog contentDialog = new ContentDialog();
                contentDialog.Title = "Action success";
                contentDialog.Content = $"Wellcome back";
                contentDialog.PrimaryButtonText = "Got it";
                await contentDialog.ShowAsync();
                this.Frame.Navigate(typeof(Pages.Demo.NavigationViewDemo));
            }
        }
    }
}
