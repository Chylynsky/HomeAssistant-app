using HomeAssistant.Model;
using HomeAssistant.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
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