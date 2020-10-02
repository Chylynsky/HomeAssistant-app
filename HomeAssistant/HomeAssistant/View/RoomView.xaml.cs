using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HomeAssistant.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RoomView : ContentView
    {
        public event EventHandler BackNavigationRequested;

        public RoomView()
        {
            InitializeComponent();

            switch (Device.RuntimePlatform)
            {
                case Device.UWP: 
                    backButton.Source = "Assets\\back.png";
                    addButton.Source = "Assets\\add.png";
                    moreButton.Source = "Assets\\more.png";
                    break;
                case Device.Android:
                    backButton.Source = "back.png";
                    addButton.Source = "add.png";
                    moreButton.Source = "more.png";
                    break;
                default: break;
            }
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

        private void deviceCard_Clicked(object sender, EventArgs e)
        {
            ShowActionCard();
        }

        private void backButton_Clicked(object sender, EventArgs e)
        {
            BackNavigationRequested.Invoke(sender, e);
        }
    }
}