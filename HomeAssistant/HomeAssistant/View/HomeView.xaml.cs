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
        }

        private void roomCard_Clicked(object sender, EventArgs e)
        {
            RoomSelected.Invoke(this, e);
        }
    }
}