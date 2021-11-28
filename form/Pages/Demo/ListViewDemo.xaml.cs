using form.Entities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace form.Pages.Demo
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ListViewDemo : Page
    {
        public ListViewDemo()
        {
            this.InitializeComponent();
            this.Loaded += Page_Loaded;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine("Load design success!");
            Debug.WriteLine("Loading data...");

            List<Account> accounts = new List<Account>();
            accounts.Add(new Account
            {
                firstName = "Cuong",
                lastName = "Tran",
                address = "Thai Binh",
                avatar = "https://via.placeholder.com/600/92c952",
                birthday = "2002-08-05",
                email = "cuong@gmail.com",
                gender = 1,
                introduction = "null1",
                phone = "0123456789",
                password = "123456"
            });
            accounts.Add(new Account
            {
                firstName = "Nhat",
                lastName = "Ha",
                address = "Thai Binh",
                avatar = "https://via.placeholder.com/600/92c952",
                birthday = "2002-02-02",
                email = "nhat@gmail.com",
                gender = 1,
                introduction = "null1",
                phone = "0111111111",
                password = "123456"
            });

            MyListView.ItemsSource = accounts;
            Debug.WriteLine("Load data success");
        }

        private void MyListView_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Debug.WriteLine("Clicked item");
            var currentIndex = MyListView.SelectedIndex;
            Debug.WriteLine($"Current index: {currentIndex}");
            Debug.WriteLine($"Selected item: {MyListView.SelectedItem}");
        }
    }
}
