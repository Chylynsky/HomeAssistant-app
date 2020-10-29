using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HomeAssistant.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CreateRoomView : ContentView
    {
        public CreateRoomView()
        {
            InitializeComponent();
        }

        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            const double ScaleToValue = 0.9;
            const int AnimationLength = 100;

            Frame senderFrame = (Frame)sender;

            if (senderFrame.Scale == ScaleToValue)
            {
                senderFrame.BorderColor = Color.LightGray;
                await senderFrame.ScaleTo(1.0, AnimationLength);
            }
            else
            {
                senderFrame.BorderColor = Color.PeachPuff;
                await senderFrame.ScaleTo(ScaleToValue, AnimationLength);
            }
        }
    }
}