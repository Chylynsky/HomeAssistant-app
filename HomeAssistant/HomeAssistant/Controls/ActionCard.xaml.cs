using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HomeAssistant.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ActionCard : Frame
    {
        public event EventHandler Closed;
        public event EventHandler<SwipedEventArgs> Swiped;

        public string Title
        {
            get => labelTitle.Text;
            set
            {
                labelTitle.Text = value;
            }
        }

        public ActionCard()
        {
            InitializeComponent();
            buttonClose.Clicked += (object sender, EventArgs e) => Closed?.Invoke(this, e);
            swipeGestureRecognizer.Swiped += (object sender, SwipedEventArgs e) => Swiped?.Invoke(this, e);
        }
    }
}