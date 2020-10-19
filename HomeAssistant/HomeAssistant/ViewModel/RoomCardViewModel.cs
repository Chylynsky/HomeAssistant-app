using HomeAssistant.Model;
using HomeAssistant.Helper;
using Xamarin.Forms;

namespace HomeAssistant.ViewModel
{
    public class RoomCardViewModel : ThemedRoomCardViewModelBase
    {
        public Command<string> SelectRoomCommand { get; set; }

        public RoomModel RoomModel { get; private set; }

        public string Name
        {
            get
            {
                return RoomModel.RoomType.ToString().SplitCamelCase();
            }
        }

        public RoomCardViewModel(RoomModel roomModel) : base(roomModel.RoomType)
        {
            RoomModel = roomModel;
        }
    }
}
