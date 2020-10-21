using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using HomeAssistant.View;
using HomeAssistant.Helper;
using System.Net;
using System.Resources;
using System.Threading.Tasks;
using HomeAssistant.ViewModel;

namespace HomeAssistant
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            Task.Run(async () => {
                var userData = await HomeAssistantClient.GetUserData();

                if (userData == null)
                {
                    await NavigationService.Navigation.NavigateToAsync<LoginViewModel>();
                    return;
                }

                await NavigationService.Navigation.NavigateToAsync<HomeViewModel>(userData);
            }).Wait();
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
