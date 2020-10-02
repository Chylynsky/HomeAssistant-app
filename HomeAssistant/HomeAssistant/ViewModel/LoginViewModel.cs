using HomeAssistant.Helper;
using System.Collections.Generic;
using Xamarin.Forms;

namespace HomeAssistant.ViewModel
{
    class LoginViewModel : ThemedViewModelBase
    {
        public Command<Dictionary<string, string>> LoginRequestCommand { get; }

        public LoginViewModel(HomeAssistantClient client)
        {
            LoginRequestCommand = new Command<Dictionary<string, string>>((credentials) => {

                if (credentials is null)
                {
                    return;
                }

                if (!credentials.ContainsKey("username"))
                {
                    return;
                }

                if (!credentials.ContainsKey("password"))
                {
                    return;
                }
            });
        }
    }
}
