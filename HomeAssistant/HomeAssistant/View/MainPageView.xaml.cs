using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using HomeAssistant.ViewModel;
using HomeAssistant.View;
using HomeAssistant.Helper;

namespace HomeAssistant
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPageView : ContentPage
    {
        private NavigationHandler navigationHandler;
        private HomeView homeView;
        private RoomView roomView;

        public MainPageView()
        {
            InitializeComponent();
            BindingContext = new MainPageViewModel();

            homeView = new HomeView();
            homeView.SetBinding(BindingContextProperty, nameof(HomeViewModel));
            homeView.RoomSelected += homeView_RoomSelected;

            roomView = new RoomView();
            roomView.SetBinding(BindingContextProperty, nameof(RoomViewModel));
            roomView.BackNavigationRequested += roomView_BackButtonClicked;

            navigationHandler = new NavigationHandler(homeView);

            Content = navigationHandler.Content;
        }

        private async void homeView_RoomSelected(object sender, EventArgs e)
        {
            await navigationHandler.NavigateToAsync(roomView);
        }

        private async void roomView_BackButtonClicked(object sender, EventArgs e)
        {
            await navigationHandler.NavigateBackAsync();
        }
    }
}
