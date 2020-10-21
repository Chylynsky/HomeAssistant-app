using HomeAssistant.Helper;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HomeAssistant.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RoomView : ContentPage
    { 
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

            actionCard.TranslationY = Bounds.Bottom;
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
            await actionCard.TranslateTo(0.0, Bounds.Bottom, 150, Easing.SinIn);
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

        private async void deviceCard_Clicked(object sender, EventArgs e)
        {
            await ShowActionCard();
        }

        private async void backButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync(true);
        }

        private void actionCard_BindingContextChanged(object sender, EventArgs e)
        {
            if (actionCard.BindingContext == null)
            {
                return;
            }

            // Create device view instance based on BindingContext type
            actionCard.InnerContent = DeviceLinker.GetDeviceViewForViewModel(actionCard.BindingContext.GetType());
            actionCard.InnerContent.BindingContext = actionCard.BindingContext;
        }
    }
}