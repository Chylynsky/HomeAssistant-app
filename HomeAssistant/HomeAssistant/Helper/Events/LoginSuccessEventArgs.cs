using HomeAssistant.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace HomeAssistant.Helper.Events
{
    public class LoginSuccessEventArgs : EventArgs
    {
        public UserModel UserModel { get; private set; }

        public LoginSuccessEventArgs(UserModel userModel)
        {
            UserModel = userModel;
        }
    }
}
