using HomeAssistant.Model;
using Xamarin.Forms;

namespace HomeAssistant.ViewModel
{
    /// <summary>
    /// Base class for RoomCardViewModel. It selects background image based on room type.
    /// </summary>
    public class ThemedRoomCardViewModelBase : ThemedViewModelBase
    {
        // Number of backgrounds available for themed category (Kitchen, Living Room, etc...)
        private static readonly int ThemedBackgroundMaxIndex = 5;

        private RoomType roomType;

        public override ImageSource Background { get; set; }

        public ThemedRoomCardViewModelBase(RoomType roomType)
        {
            this.roomType = roomType;

            if (roomType == RoomType.Other)
            {
                Background = base.GetImage();
            }
            else
            {
                Background = GetImage();
            }
        }

        /// <summary>
        /// Select background image randomly, based on room type.
        /// </summary>
        /// <returns>ImageSource object being the selected image path.</returns>
        protected override ImageSource GetImage()
        {
            string image = roomType.ToString().ToLower() +
            randomGenerator.Next(0, ThemedBackgroundMaxIndex).ToString() + ".png";

            switch (Device.RuntimePlatform)
            {
                case Device.iOS:
                    return default(ImageSource);
                case Device.Android:
                    return image;
                case Device.UWP: return ResourcePathUWP + image;
                default: return default(ImageSource);
            }
        }
    }
}
