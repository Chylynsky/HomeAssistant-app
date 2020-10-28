using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;
using Xamarin.Forms;
using Xamarin.Forms.Markup;
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

        public bool IsUp
        {
            get
            {
                return TranslationY != Application.Current.MainPage.Bounds.Bottom;
            }
            set
            {
                if (!value)
                {
                    TranslationY = Application.Current.MainPage.Bounds.Bottom;
                }
                else
                {
                    TranslationY = 0.0;
                }
            }
        }


        public ActionCard()
        {
            InitializeComponent();
            
            switch (Device.RuntimePlatform)
            {
                case Device.UWP: closeButton.Source = "Assets\\close.png"; break;
                case Device.Android: closeButton.Source = "close.png"; break;
                default: break;
            }

            closeButton.Clicked += async (object sender, EventArgs e) => {
                await SlideDown();
                Closed?.Invoke(this, e); 
            };
            swipeGestureRecognizer.Swiped += async (object sender, SwipedEventArgs e) => {
                await SlideDown();
                Swiped?.Invoke(this, e); 
            };
        }

        public async Task SlideUp()
        {
            IsVisible = true;
            await this.TranslateTo(0.0, 0.0, 150, Easing.SinOut);
            await this.ScaleTo(1.0, 75, Easing.SinOut);
            IsEnabled = true;
        }

        public async Task SlideDown()
        {
            IsEnabled = false;
            await this.ScaleTo(0.8, 75, Easing.SinIn);
            await this.TranslateTo(0.0, Application.Current.MainPage.Bounds.Bottom, 150, Easing.SinIn);
            IsVisible = false;
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