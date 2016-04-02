using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Lepra
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private DataService _dataService;

        public MainPage()
        {
            this.InitializeComponent();

            _dataService = ((App) App.Current).DataService;            
        }
        
        private async void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            var username = txtUsername.Text;
            var password = txtPassword.Password;

            if(String.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                return;

            var authResult = await _dataService.SignIn(username, password);

            if (authResult.Status != "OK")
            {
                txtError.Visibility = Visibility.Visible;
                txtError.Text = authResult.Errors.FirstOrDefault() != null ? authResult.Errors.FirstOrDefault().Code : "Unknown Error";
                return;
            }

            ApplicationData.Current.LocalSettings.Values["AuthToken"] = authResult.CsrfToken;

            Frame.Navigate(typeof (IndexPage));
        }
    }
}
