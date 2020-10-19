using HomeAssistant.Controls;
using HomeAssistant.ViewModel;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HomeAssistant.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomeView : ContentPage
    {
        public HomeView()
        {
            InitializeComponent();
            BindingContext = new HomeViewModel();

            switch (Device.RuntimePlatform)
            {
                case Device.UWP:
                    addButton.Source = "Assets\\add.png";
                    moreButton.Source = "Assets\\more.png";
                    break;
                case Device.Android:
                    addButton.Source = "add.png";
                    moreButton.Source = "more.png";
                    break;
                default: break;
            }

            HideActionCard();
        }

        private void roomCard_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new RoomView()
            {
                BindingContext = ((HomeViewModel)BindingContext).SelectedRoomViewModel
            }, true);
        }

        private async Task ShowActionCard()
        {
            actionCard.IsVisible = true;
            await actionCard.TranslateTo(0.0, 0.0, 150, Easing.SinOut);
            await actionCard.ScaleTo(1.0, 75, Easing.SinOut);
            actionCard.IsEnabled = true;
        }

        private async Task HideActionCard()
        {
            actionCard.IsEnabled = false;
            await actionCard.ScaleTo(0.8, 75, Easing.SinIn);
            await actionCard.TranslateTo(0.0, mainGrid.Bounds.Bottom, 150, Easing.SinIn);
            actionCard.IsVisible = false;
        }

        private async void actionCard_Closed(object sender, EventArgs e)
        {
            await HideActionCard();
        }

        private async void actionCard_Swiped(object sender, SwipedEventArgs e)
        {
            if (e.Direction != SwipeDirection.Down)
            {
                return;
            }

            await HideActionCard();
        }

        private async void addButton_Clicked(object sender, EventArgs e)
        {
            if (actionCard.IsEnabled)
            {
                await HideActionCard();
            }

            actionCard.BindingContext = new CreateRoomActionViewModel();
            actionCard.InnerContent = new CreateRoomActionView();
            actionCard.InnerContent.BindingContext = actionCard.BindingContext;

            await ShowActionCard();
        }

        private async void moreButton_Clicked(object sender, EventArgs e)
        {
            if (actionCard.IsEnabled)
            {
                await HideActionCard();
            }

            await ShowActionCard();
        }

        private void actionCard_BindingContextChanged(object sender, EventArgs e)
        {
            return;
        }
    }
}