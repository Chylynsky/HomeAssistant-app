﻿using HomeAssistant.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HomeAssistant.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginView : ContentView
    {
        public event EventHandler LoginSuccess;

        public static readonly BindableProperty UserAuthenticatedPropety = BindableProperty.Create(
            nameof(UserAuthenticated),
            typeof(bool),
            typeof(LoginView),
            false);

        public bool UserAuthenticated
        {
            get
            {
                return (bool)GetValue(UserAuthenticatedPropety);
            }
            set
            {
                SetValue(UserAuthenticatedPropety, value);
            }
        }

        public LoginView()
        {
            InitializeComponent();
            SetBinding(UserAuthenticatedPropety, new Binding("UserAuthenticated"));
        }

        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);

            if (propertyName == UserAuthenticatedPropety.PropertyName)
            {
                LoginSuccess?.Invoke(this, null);
            }
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