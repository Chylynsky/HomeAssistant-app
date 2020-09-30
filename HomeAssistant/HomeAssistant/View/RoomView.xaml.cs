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

        private void SwipeGestureRecognizer_Swiped(object sender, SwipedEventArgs e)
        {
            BackNavigationRequested.Invoke(sender, e);
        }

        private void backButton_Clicked(object sender, EventArgs e)
        {
            BackNavigationRequested.Invoke(sender, e);
        }
    }
}