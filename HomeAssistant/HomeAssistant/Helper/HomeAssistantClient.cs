using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using HomeAssistant.Model;
using Newtonsoft.Json;

namespace HomeAssistant.Helper
{
    class HomeAssistantClient
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

        public async Task<ObservableCollection<DeviceBase>> GetConnectedDevices()
        {
            Thread.Sleep(10);
            var deviceList = new ObservableCollection<DeviceBase>();

            /*HttpResponseMessage response = await httpClient.GetAsync("devices", HttpCompletionOption.ResponseContentRead);

            if (!response.IsSuccessStatusCode)
            {
                return deviceList;
            }

            string responseContent = await response.Content.ReadAsStringAsync();
            var devicesList = JsonConvert.DeserializeObject<List<Dictionary<string, string>>>(responseContent);

            foreach (var deviceEntry in devicesList)
            {
                var deviceFactory = deviceFactoryDictionary[deviceEntry["type"]];
                var device = deviceFactory.Create();

                device.Id = deviceEntry["id"];
                device.Name = deviceEntry["name"];

                deviceList.Add(device);
            }*/

            return deviceList;
        }
    }
}
