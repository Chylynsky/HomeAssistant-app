﻿using HomeAssistant.Helper;
using HomeAssistant.Helper.Events;
using System;
using System.ComponentModel;
using System.Net;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace HomeAssistant.ViewModel
{
    public class LoginViewModel : ThemedViewModelBase, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public Command LoginRequestCommand { get; }

        public string Username { get; set; }

        public string Password { get; set; }

        public LoginViewModel()
        {
            // Try to get user data, change UserAuthenticated property on success
            Task.Run(async () => {
                var userData = await HomeAssistantClient.GetUserData();

                if (userData == null)
                {
                    return;
                }

                await NavigationService.Navigation.NavigateToAsync<HomeViewModel>();
            });

            LoginRequestCommand = new Command(async () => {

                if (string.IsNullOrEmpty(Username))
                {
                    return;
                }

                if (string.IsNullOrEmpty(Password))
                {
                    return;
                }

                HttpStatusCode status = await HomeAssistantClient.RequestLogin(Username, Password);

                switch (status)
                {
                    case HttpStatusCode.OK:
                        await NavigationService.Navigation.NavigateToAsync<HomeViewModel>();
                        return;
                    case HttpStatusCode.RequestTimeout:
                        await Application.Current.MainPage.DisplayAlert("Connection error.", "Connection timed out.", "OK");
                        return;
                    case HttpStatusCode.Unauthorized:
                        await Application.Current.MainPage.DisplayAlert("Authorization error.", "Provided credentials are invalid.", "OK");
                        return;
                    case HttpStatusCode.NotFound:
                        await Application.Current.MainPage.DisplayAlert("Internal server error.", "Internal server error occured. Try again later.", "OK");
                        return;
                    default:
                        await Application.Current.MainPage.DisplayAlert("Unknown error.", "Unknown error occured.", "OK");
                        return;
                }
            });
        }

        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
