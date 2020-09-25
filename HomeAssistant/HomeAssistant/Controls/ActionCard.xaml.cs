using System;
using System.Runtime.CompilerServices;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HomeAssistant.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ActionCard : Frame
    {
        public static readonly BindableProperty TitleProperty = BindableProperty.Create(
            nameof(Title),
            typeof(string),
            typeof(ActionCard),
            default(string));

        public static new readonly BindableProperty ContentProperty = BindableProperty.Create(
            nameof(Content),
            typeof(View),
            typeof(ActionCard),
            default(View));

        public event EventHandler Closed;
        public event EventHandler<SwipedEventArgs> Swiped;

        public string Title
        {
            get
            {
                return (string)GetValue(TitleProperty);
            }
            set
            {
                SetValue(TitleProperty, value);
            }
        }

        public new View Content
        {
            get
            {
                return (View)GetValue(ContentProperty);
            }
            set
            {
                SetValue(ContentProperty, value);
            }
        }

        public ActionCard()
        {
            InitializeComponent();
            closeButton.Clicked += (object sender, EventArgs e) => Closed?.Invoke(this, e);
            swipeGestureRecognizer.Swiped += (object sender, SwipedEventArgs e) => Swiped?.Invoke(this, e);

            scrollViewContent.Content
        }

        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);

            if (propertyName == TitleProperty.PropertyName)
            {
                titleLabel.Text = Title;
            }
            else if (propertyName == ContentProperty.PropertyName)
            {
                scrollViewContent.Content = Content;
            }
        }
    }
}