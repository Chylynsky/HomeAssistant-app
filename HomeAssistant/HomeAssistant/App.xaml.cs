using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using HomeAssistant.View;
using HomeAssistant.Helper;
using System.Net;
using System.Resources;
using System.Threading.Tasks;
using HomeAssistant.ViewModel;
using System.Collections.Generic;
using HomeAssistant.Model;
using System.Net.Http;

namespace HomeAssistant
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            // Setting page on application start allows the use of Page boundaries in later navigation
            MainPage = new ContentPage
            {
                BackgroundImageSource = @"Assets\\universal0.png"
            };

            Device.BeginInvokeOnMainThread(async () =>
            {
                UserModel userData = await HomeAssistantHttpClient.GetUserData();

                if (userData == null)
                {
                    await NavigationService.Navigation.NavigateToAsync<LoginViewModel>();
                    return;
                }

                await NavigationService.Navigation.NavigateToAsync<HomeViewModel>(userData);
            });
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
