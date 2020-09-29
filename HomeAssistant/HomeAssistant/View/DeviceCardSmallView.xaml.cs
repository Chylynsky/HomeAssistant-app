using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HomeAssistant.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DeviceCardSmallView : Frame
    {
        public event EventHandler Clicked;

        public DeviceCardSmallView()
        {
            InitializeComponent();
        }

        private async void tapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            Clicked?.Invoke(this, e);
            await this.ScaleTo(0.9, 50, Easing.SinOut);
            await this.ScaleTo(1.0, 50, Easing.SinIn);
        }
    }
}