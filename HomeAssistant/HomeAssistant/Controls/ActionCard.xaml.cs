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

        public static readonly BindableProperty TitleFontColorProperty = BindableProperty.Create(
            nameof(TitleFontColor),
            typeof(Color),
            typeof(ActionCard),
            default(Color));
        
        public static readonly BindableProperty InnerContentProperty = BindableProperty.Create(
            nameof(InnerContent),
            typeof(Xamarin.Forms.View),
            typeof(ActionCard),
            default(Xamarin.Forms.View));

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

        public Color TitleFontColor
        {
            get
            {
                return (Color)GetValue(TitleFontColorProperty);
            }
            set
            {
                SetValue(TitleFontColorProperty, value);
            }
        }

        public Xamarin.Forms.View InnerContent
        {
            get
            {
                return (Xamarin.Forms.View)GetValue(InnerContentProperty);
            }
            set
            {
                SetValue(InnerContentProperty, value);
            }
        }

        public ActionCard()
        {
            InitializeComponent();
            closeButton.Clicked += (object sender, EventArgs e) => Closed?.Invoke(this, e);
            swipeGestureRecognizer.Swiped += (object sender, SwipedEventArgs e) => Swiped?.Invoke(this, e);
        }

        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);

            if (propertyName == TitleProperty.PropertyName)
            {
                titleLabel.Text = Title;
            }
            else if (propertyName == TitleFontColorProperty.PropertyName)
            {
                titleLabel.TextColor = TitleFontColor;
            }
            else if (propertyName == InnerContentProperty.PropertyName)
            {
                scrollViewContent.Content = InnerContent;
            }
        }
    }
}