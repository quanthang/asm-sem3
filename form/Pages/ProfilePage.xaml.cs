using form.Service;
using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace form.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ProfilePage : Page
    {
        private AccountService accountService = new AccountService();
        public ProfilePage()
        {
            this.InitializeComponent();
            this.Loaded += ProfilePage_Loaded;
        }

        private async void ProfilePage_Loaded(object sender, RoutedEventArgs e)
        {
            var account = await accountService.GetInformationAsync();
            if (account == null)
            {
                ContentDialog contentDialog = new ContentDialog();
                contentDialog.Title = "Login required";
                contentDialog.Content = $"Please login to continue!";
                contentDialog.PrimaryButtonText = "Got it!";
                await contentDialog.ShowAsync();
                Frame.Navigate(typeof(Pages.LoginPage));
            }
            else
            {
                Email.Text = account.email;
                FullName.Text = account.firstName + " " + account.lastName;
            }
        }
    }
}
