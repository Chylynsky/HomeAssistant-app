using HomeAssistant.ViewModel;
using System;
using System.Runtime.CompilerServices;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HomeAssistant.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginView : ContentPage
    {
        public LoginView()
        {
            InitializeComponent();
        }

        private void registerButton_Clicked(object sender, EventArgs e)
        {
            
        }

        private void loginButton_Clicked(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(usernameEntry.Text))
            {
                usernameEntry.BackgroundColor = Color.DarkRed;
            }

            if (string.IsNullOrEmpty(passwordEntry.Text))
            {
                passwordEntry.BackgroundColor = Color.DarkRed;
            }
        }

        private void usernameEntry_TextChanged(object sender, TextChangedEventArgs e)
        {
            usernameEntry.BackgroundColor = Color.DarkGray;
        }

        private void passwordEntry_TextChanged(object sender, TextChangedEventArgs e)
        {
            passwordEntry.BackgroundColor = Color.DarkGray;
        }
    }
}