using HomeAssistant.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HomeAssistant.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginView : ContentView
    {
        public LoginView()
        {
            InitializeComponent();
            BindingContext = new LoginViewModel();
            loginButton.CommandParameter = new Dictionary<string, string>();
        }

        private void registerButton_Clicked(object sender, EventArgs e)
        {
            
        }

        private void loginButton_Clicked(object sender, EventArgs e)
        {
            if (usernameEntry.Text?.Length == 0 || usernameEntry.Text is null)
            {
                usernameEntry.BackgroundColor = Color.DarkRed;
            }
            else
            {
                ((Dictionary<string, string>)loginButton.CommandParameter)["username"] = usernameEntry.Text;
            }

            if (passwordEntry.Text?.Length == 0 || passwordEntry.Text is null)
            {
                passwordEntry.BackgroundColor = Color.DarkRed;
            }
            else
            {
                ((Dictionary<string, string>)loginButton.CommandParameter)["password"] = passwordEntry.Text;
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