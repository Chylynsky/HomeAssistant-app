using HomeAssistant.Controls;
using HomeAssistant.ViewModel;
using System;
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
        }

        private void roomCard_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new RoomView()
            {
                BindingContext = ((HomeViewModel)BindingContext).SelectedRoomViewModel
            }, true);
        }

        private async void ShowActionCard()
        {
            actionCard.IsVisible = true;
            await actionCard.TranslateTo(0.0, 0.0, 150, Easing.SinOut);
            await actionCard.ScaleTo(1.0, 75, Easing.SinOut);
            actionCard.IsEnabled = true;
        }

        private async void HideActionCard()
        {
            actionCard.IsEnabled = false;
            await actionCard.ScaleTo(0.8, 75, Easing.SinIn);
            await actionCard.TranslateTo(0.0, mainGrid.Bounds.Bottom, 150, Easing.SinIn);
            actionCard.IsVisible = false;
        }

        private void actionCard_Closed(object sender, EventArgs e)
        {
            HideActionCard();
        }

        private void actionCard_Swiped(object sender, SwipedEventArgs e)
        {
            if (e.Direction != SwipeDirection.Down)
            {
                return;
            }

            HideActionCard();
        }

        private void addButton_Clicked(object sender, EventArgs e)
        {
            if (actionCard.IsEnabled)
            {
                HideActionCard();
            }

            // Bind both action card and its content
            actionCard.BindingContext = new CreateRoomActionViewModel();
            actionCard.InnerContent.BindingContext = actionCard.BindingContext;

            ShowActionCard();
        }

        private void moreButton_Clicked(object sender, EventArgs e)
        {
            if (actionCard.IsEnabled)
            {
                HideActionCard();
            }

            ShowActionCard();
        }
    }
}