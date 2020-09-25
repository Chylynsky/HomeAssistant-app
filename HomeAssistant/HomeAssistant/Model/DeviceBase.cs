using Xamarin.Forms;

namespace HomeAssistant.Model
{
    abstract class DeviceBase
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string DeviceTypename { get; protected set; }
        public ImageSource IconSource { get; protected set; }

        public DeviceBase()
        {

        }
    }
}
