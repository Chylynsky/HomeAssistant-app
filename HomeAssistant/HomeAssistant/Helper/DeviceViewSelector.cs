using HomeAssistant.View.DeviceViews;
using HomeAssistant.ViewModel.DeviceViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace HomeAssistant.Helper
{
    public class DeviceViewSelector : Dictionary<Type, Func<ContentView>>
    {
        public DeviceViewSelector()
        {
            this[typeof(MiKettleViewModel)] = () => { return new MiKettleView(); };
        }
    }
}
