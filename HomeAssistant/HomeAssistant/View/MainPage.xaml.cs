using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;
using HomeAssistant.ViewModel;
using Xamarin.Forms.Markup;

namespace HomeAssistant
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            BindingContext = new MainPageViewModel();
        }

        private async void ShowActionCard()
        {
            actionCard.IsVisible = true;
            await actionCard.TranslateTo(0.0, 0.0, 200, Easing.SinOut);
            await actionCard.ScaleTo(1.0, 100, Easing.SinOut);
            actionCard.IsEnabled = true;
        }

        private async void HideActionCard()
        {
            actionCard.IsEnabled = false;
            await actionCard.ScaleTo(0.8, 100, Easing.SinIn);
            await actionCard.TranslateTo(0.0, mainGrid.Bounds.Bottom, 200, Easing.SinIn);
            actionCard.IsVisible = false;
        }

        private void actionCard_Closed(object sender, EventArgs e)
        {
            HideActionCard();
        }
        /*
        private async void buttonAdd_Clicked(object sender, EventArgs e)
        {
            Vibration.Vibrate(5.0);
            actionCard.IsVisible = true;
            await actionCard.TranslateTo(0.0, 0.0, 200, Easing.SinOut);
            await actionCard.ScaleTo(1.0, 100, Easing.SinOut);
            actionCard.IsEnabled = true;
        }
        */
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
    }
}
