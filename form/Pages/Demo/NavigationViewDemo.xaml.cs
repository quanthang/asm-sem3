using System.Diagnostics;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace form.Pages.Demo
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class NavigationViewDemo : Page
    {
        private bool IsAdmin = false;
        public NavigationViewDemo()
        {
            this.InitializeComponent();
            this.contentFrame.Navigate(typeof(Pages.RegisterPage));
        }

        private void NavigationView_SelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
        {
            if (args.IsSettingsSelected)
            {
                Debug.WriteLine("Select setting.");
            }
            var navigationViewItem = args.SelectedItem as NavigationViewItem;
            switch (navigationViewItem.Tag)
            {
                case "Login":
                    this.contentFrame.Navigate(typeof(Pages.LoginPage));
                    break;
                case "Register":
                    this.contentFrame.Navigate(typeof(Pages.RegisterPage));
                    break;
                case "Profile":
                    this.contentFrame.Navigate(typeof(Pages.ProfilePage));
                    break;
                case "ListSong":
                    this.contentFrame.Navigate(typeof(Pages.ListSong));
                    break;
                case "Createsong":
                    this.contentFrame.Navigate(typeof(Pages.CreateSong));
                    break;
            }
        }
    }
}
