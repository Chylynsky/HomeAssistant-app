using HomeAssistant.Model;
using HomeAssistant.ViewModel.DeviceViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace HomeAssistant.Helper
{
    // Class matching DeviceModel with its View Model.
    class DeviceViewModelSelector : Dictionary<Type, Func<DeviceModelBase, DeviceViewModelBase>>
    {
        public DeviceViewModelSelector()
        {
            this[typeof(MiKettleModel)] = (DeviceModelBase deviceModel) => { return new MiKettleViewModel(deviceModel); };
        }
    }
}
