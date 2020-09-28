using HomeAssistant.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace HomeAssistant.ViewModel
{
    class DeviceCardSmallViewModel
    {
        public Command<string> SelectDeviceCommand { get; set; }

        public DeviceModel Device { get; private set; }

        public string Id
        {
            get
            {
                return Device.Id;
            }
        }

        public string Name
        {
            get
            {
                return Device.Name;
            }
        }

        public ImageSource IconSource
        {
            get
            {
                return Device.IconSource;
            }
        }

        public DeviceCardSmallViewModel(DeviceModel device)
        {
            Device = device;
        }
    }
}
