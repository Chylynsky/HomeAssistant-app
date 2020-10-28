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
        }

        private async void deviceCard_Clicked(object sender, EventArgs e)
        {
            await actionCard.SlideUp();
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

            if (actionCard.InnerContent != null && actionCard.BindingContext == actionCard.InnerContent.BindingContext)
            {
                return;
            }

            // Create device view instance based on BindingContext type
            actionCard.InnerContent = DeviceLinker.GetDeviceViewForViewModel(actionCard.BindingContext.GetType());
        }
    }
}