using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using Xamarin.Forms;

namespace HomeAssistant.ViewModel
{
    public class ThemedViewModelBase : IThemedViewModelBase
    {
        // Number of backgrounds available for Universal category
        protected static readonly int UniversalBackgroundMaxIndex = 10;

        protected static readonly string ResourcePathUWP = "Assets\\Images\\";

        protected static Random randomGenerator;

        public virtual ImageSource Background { get; set; }

        static ThemedViewModelBase()
        {
            randomGenerator = new Random();
        }

        public ThemedViewModelBase()
        {
            Background = GetImage();
        }

        protected virtual ImageSource GetImage()
        {
            string image = "universal" + randomGenerator.Next(0, UniversalBackgroundMaxIndex).ToString() + ".jpg";

            switch (Device.RuntimePlatform)
            {
                case Device.iOS:
                    return default(ImageSource);
                case Device.Android:
                    return image;
                case Device.UWP: return ResourcePathUWP + image;
                default: return default(ImageSource);
            }
        }
    }
}
