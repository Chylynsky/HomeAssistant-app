﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Collections.ObjectModel;
using HomeAssistant.ViewModel;
using HomeAssistant.Model;
using System.Runtime.CompilerServices;

namespace HomeAssistant.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RoomView : ContentView
    {
        public event EventHandler BackButtonClicked;

        public RoomView()
        {
            InitializeComponent();
        }

        private async void ShowActionCard()
        {
            actionCard.IsVisible = true;
            await actionCard.TranslateTo(0.0, 0.0, 100, Easing.SinOut);
            await actionCard.ScaleTo(1.0, 50, Easing.SinOut);
            actionCard.IsEnabled = true;
        }

        private async void HideActionCard()
        {
            actionCard.IsEnabled = false;
            await actionCard.ScaleTo(0.8, 50, Easing.SinIn);
            await actionCard.TranslateTo(0.0, mainGrid.Bounds.Bottom, 100, Easing.SinIn);
            actionCard.IsVisible = false;
        }

        private void actionCard_Closed(object sender, EventArgs e)
        {
            HideActionCard();
        }

        private void actionCard_Swiped(object sender, SwipedEventArgs e)
        {
            if (e.Direction != SwipeDirection.Down)
            {
                return;
            }

            HideActionCard();
        }

        private void deviceCard_Clicked(object sender, EventArgs e)
        {
            ShowActionCard();
        }

        private void backButton_Clicked(object sender, EventArgs e)
        {
            BackButtonClicked.Invoke(sender, e);
        }
    }
}