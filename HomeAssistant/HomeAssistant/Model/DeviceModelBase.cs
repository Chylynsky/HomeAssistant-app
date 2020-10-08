using Xamarin.Forms;

namespace HomeAssistant.Model
{
    public abstract class DeviceModelBase
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public ImageSource IconSource { get; protected set; }
    }
}
