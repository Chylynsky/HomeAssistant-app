using HomeAssistant.View.DeviceViews;
using HomeAssistant.ViewModel.DeviceViewModels;
using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace HomeAssistant.Helper
{
    // Class that matches Device View with its View Model.
    public class DeviceViewSelector : Dictionary<Type, Func<ContentView>>
    {
        public DeviceViewSelector()
        {
            this[typeof(MiKettleViewModel)] = () => { return new MiKettleView(); };
        }
    }
}
