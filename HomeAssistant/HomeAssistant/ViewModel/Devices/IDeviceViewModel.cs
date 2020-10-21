using HomeAssistant.Model.Devices;
using System.ComponentModel;

namespace HomeAssistant.ViewModel.Devices
{
    public interface IDeviceViewModel : INotifyPropertyChanged
    {
        string Name { get; }

        string Id { get; }

        IDeviceModel DeviceModel { get; }

        void UpdateData();
    }
}
