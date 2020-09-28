﻿using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;
using HomeAssistant.ViewModel;
using Xamarin.Forms.Markup;
using HomeAssistant.View;
using System.Threading.Tasks;

namespace HomeAssistant
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            BindingContext = new MainPageViewModel();
            roomView.BindingContext = BindingContext;
            homeView.BindingContext = BindingContext;

            roomView.TranslationX = homeView.Width;
        }

        private async Task NavigateTo(ContentView contentView)
        {
            if (contentView == roomView)
            {
                await Task.WhenAll(
                    homeView.TranslateTo(-40.0, 0.0, 150, Easing.SinOut),
                    roomView.TranslateTo(0.0, 0.0, 150, Easing.SinOut));
            }
            else
            {
                await Task.WhenAll(
                    homeView.TranslateTo(0.0, 0.0, 150, Easing.SinOut),
                    roomView.TranslateTo(homeView.Width, 0.0, 150, Easing.SinOut));
            }
        }

        private async void mainView_RoomSelected(object sender, EventArgs e)
        {
            await NavigateTo(roomView);
        }

        private async void roomView_BackButtonClicked(object sender, EventArgs e)
        {
            await NavigateTo(homeView);
        }
    }
}
