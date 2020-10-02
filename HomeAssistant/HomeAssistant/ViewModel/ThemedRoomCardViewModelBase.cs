using HomeAssistant.Model;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace HomeAssistant.ViewModel
{
    public class ThemedRoomCardViewModelBase : ThemedViewModelBase
    {
        // Number of backgrounds available for themed category (Kitchen, Living Room, etc...)
        private static readonly int ThemedBackgroundMaxIndex = 5;

        private RoomType roomType;

        public override ImageSource Background
        {
            get
            {
                if (roomType == RoomType.Other)
                    return base.Background;

                string image = roomType.ToString().ToLower() + 
                    randomGenerator.Next(0, ThemedBackgroundMaxIndex).ToString() + ".jpg";

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

        public ThemedRoomCardViewModelBase(RoomType roomType)
        {
            this.roomType = roomType;
        }
    }
}
