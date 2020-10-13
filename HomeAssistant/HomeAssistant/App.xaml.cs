﻿using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using HomeAssistant.View;
using HomeAssistant.Helper;
using System.Net;
using System.Resources;

namespace HomeAssistant
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            MainPage = new NavigationPage(new LoginView());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
