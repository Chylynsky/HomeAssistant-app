using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HomeAssistant.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomeView : ContentView
    {
        public event EventHandler RoomSelected;

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

        private void roomCard_Clicked(object sender, EventArgs e)
        {
            RoomSelected.Invoke(this, e);
        }
    }
}