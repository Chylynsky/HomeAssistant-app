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

        private async void addButton_Clicked(object sender, EventArgs e)
        {
            if (actionCard.IsEnabled)
            {
                await actionCard.SlideDown();
            }

            await actionCard.SlideUp();
        }

        private void moreButton_Clicked(object sender, EventArgs e)
        {
            return;
        }

        private void actionCard_BindingContextChanged(object sender, EventArgs e)
        {
            return;
        }
    }
}