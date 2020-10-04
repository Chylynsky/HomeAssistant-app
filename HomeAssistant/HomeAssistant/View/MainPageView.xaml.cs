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
            homeView.SetBinding(BindingContextProperty, nameof(MainPageViewModel.HomeViewModel));
            homeView.RoomSelected += async (sender, args) => { await navigationHandler.NavigateToAsync(roomView); };

            roomView = new RoomView();
            roomView.SetBinding(BindingContextProperty, nameof(MainPageViewModel.SelectedRoomViewModel));
            roomView.BackNavigationRequested += async (sender, args) => { await navigationHandler.NavigateBackAsync(); };

            var loginView = new LoginView();
            loginView.SetBinding(BindingContextProperty, nameof(MainPageViewModel.LoginViewModel));
            loginView.LoginSuccess += async (sender, args) => { await navigationHandler.NavigateToAsync(homeView); };

            navigationHandler = new NavigationHandler(loginView);

            Content = navigationHandler.Content;
        }
    }
}
