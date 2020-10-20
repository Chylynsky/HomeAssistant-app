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

            var navigationService = NavigationService.Navigation;
            navigationService.NavigateToAsync<LoginViewModel>().Wait();
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
