﻿using System;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HomeAssistant.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DeviceCardSmall : Frame
    {
        public static readonly BindableProperty TitleProperty = BindableProperty.Create(
            nameof(Title), 
            typeof(string),
            typeof(DeviceCardSmall),
            default(string));

        public static readonly BindableProperty TitleFontColorProperty = BindableProperty.Create(
            nameof(TitleFontColor),
            typeof(Color),
            typeof(DeviceCardSmall),
            default(Color));

        public static readonly BindableProperty IconSourceProperty = BindableProperty.Create(
            nameof(IconSource),
            typeof(ImageSource),
            typeof(DeviceCardSmall),
            default(ImageSource));

        public static readonly BindableProperty CommandProperty = BindableProperty.Create(
            nameof(Command),
            typeof(ICommand),
            typeof(DeviceCardSmall),
            default(ICommand));

        public static readonly BindableProperty CommandParameterProperty = BindableProperty.Create(
            nameof(CommandParameter),
            typeof(object),
            typeof(DeviceCardSmall),
            default(object));

        public event EventHandler Clicked;

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

        public ImageSource IconSource
        {
            get
            {
                return (ImageSource)GetValue(IconSourceProperty);
            }
            set
            {
                SetValue(IconSourceProperty, value);
            }
        }

        public ICommand Command
        {
            get
            {
                return (ICommand)GetValue(CommandProperty);
            }
            set
            {
                SetValue(CommandProperty, value);
            }
        }

        public object CommandParameter
        {
            get
            {
                return GetValue(CommandParameterProperty);
            }
            set
            {
                SetValue(CommandParameterProperty, value);
            }
        }

        public DeviceCardSmall()
        {
            InitializeComponent();
            tapGestureRecognizer.Tapped += async (object sender, EventArgs e) => {
                Command?.Execute(CommandParameter);
                Clicked?.Invoke(this, e);
                await this.ScaleTo(0.9, 40, Easing.SinOut);
                await this.ScaleTo(1.0, 40, Easing.SinIn);
            };
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
            else if (propertyName == IconSourceProperty.PropertyName)
            {
                deviceIcon.Source = IconSource;
            }
        }
    }
}