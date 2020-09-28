using Xamarin.Forms;

namespace HomeAssistant.Model
{
    public abstract class DeviceModel
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public ImageSource IconSource { get; protected set; }
    }
}
