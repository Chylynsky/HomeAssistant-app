﻿using HomeAssistant.Model;
using HomeAssistant.ViewModel.DeviceViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HomeAssistant.View.DeviceViews
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MiKettleView : ContentView
    {
        public MiKettleView()
        {
            InitializeComponent();
        }
    }
}