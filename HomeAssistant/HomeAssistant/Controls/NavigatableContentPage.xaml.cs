using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HomeAssistant.Controls
{
    public partial class NavigatableContentPage : ContentPage
    {
        private static readonly uint AnimationLength = 200U;

        public NavigatableContentPage() : base()
        {
            TranslationX = Width;
        }

        protected override void OnAppearing()
        {
            this.TranslateTo(0.0, 0.0, AnimationLength, Easing.SinOut);
            base.OnAppearing();
        }

        protected override void OnDisappearing()
        {
            this.TranslateTo(Width, 0.0, AnimationLength, Easing.SinOut);
            base.OnDisappearing();
        }
    }
}
