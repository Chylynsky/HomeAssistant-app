using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using HomeAssistant.Model;
using HomeAssistant.ViewModel;
using Newtonsoft.Json;

namespace HomeAssistant.Helper
{
    public class HomeAssistantClient
    {
        private static readonly DeviceFactoryDictionary deviceFactoryDictionary = new DeviceFactoryDictionary();

        private HttpClient httpClient;

        private HttpClientHandler clientHandler;

        public WebProxy Proxy
        {
            get 
            {
                return (WebProxy)clientHandler.Proxy;
            }
            set
            {
                clientHandler.Proxy = value;
            }
        }

        public Uri Address
        {
            get
            {
                return httpClient.BaseAddress;
            }
            set
            {
                httpClient.BaseAddress = value;
            }
        }

        public HomeAssistantClient(Uri address = null, WebProxy proxy = null)
        {
            clientHandler = new HttpClientHandler();

            if (proxy != null)
            {
                Proxy = proxy;
            }

            httpClient = new HttpClient(clientHandler);

            if (address != null)
            {
                Address = address;
            }
        }

        public async Task<List<DeviceModel>> GetConnectedDevices()
        {
            var deviceList = new List<DeviceModel>();

            HttpResponseMessage response = await httpClient.GetAsync("devices", HttpCompletionOption.ResponseContentRead);

            if (!response.IsSuccessStatusCode)
            {
                return deviceList;
            }

            string responseContent = await response.Content.ReadAsStringAsync();
            var devicesList = JsonConvert.DeserializeObject<List<Dictionary<string, string>>>(responseContent);

            foreach (var deviceEntry in devicesList)
            {
                var deviceFactory = deviceFactoryDictionary[deviceEntry["Type"]];
                var device = deviceFactory.Create();

                device.Id = deviceEntry["Id"];
                device.Name = deviceEntry["Name"];

                deviceList.Add(device);
            }

            return deviceList;
        }

        // Method assumes the credentials are validated
        public async Task<UserModel> RequestLogin(string username, string password)
        {
            Dictionary<string, string> credentials = new Dictionary<string, string>();
            credentials["Username"] = username;
            credentials["Password"] = password;

            string serializedData = JsonConvert.SerializeObject(credentials);
            StringContent content = new StringContent(serializedData, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await httpClient.PostAsync("login", content);

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            serializedData = await response.Content.ReadAsStringAsync();
            UserModel responseData = JsonConvert.DeserializeObject<UserModel>(serializedData);

            return responseData;
        }
    }
}
