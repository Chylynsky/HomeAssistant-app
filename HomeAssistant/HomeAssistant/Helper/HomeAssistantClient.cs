using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using HomeAssistant.Model;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace HomeAssistant.Helper
{
    public static class HomeAssistantClient
    {
        private static HttpClient httpClient;

        private static HttpClientHandler clientHandler;

        public static WebProxy Proxy
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

        public static Uri Address
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

        static HomeAssistantClient()
        {
            clientHandler = new HttpClientHandler();
            Proxy = new WebProxy(HomeAssistant.Properties.Resources.Proxy);

            httpClient = new HttpClient(clientHandler);
            Address = new Uri("http://home.as");
        }

        public static async Task<List<DeviceModelBase>> GetConnectedDevices()
        {
            var deviceList = new List<DeviceModelBase>();

            HttpResponseMessage response = await httpClient.GetAsync("devices", HttpCompletionOption.ResponseContentRead);

            if (!response.IsSuccessStatusCode)
            {
                return deviceList;
            }

            string responseContent = await response.Content.ReadAsStringAsync();
            var devicesList = JsonConvert.DeserializeObject<List<Dictionary<string, string>>>(responseContent);

            DeviceModelSelector deviceModelSelector = new DeviceModelSelector();

            foreach (var deviceEntry in devicesList)
            {
                var deviceFactory = deviceModelSelector[deviceEntry["Type"]];
                var device = deviceFactory.Invoke();

                device.Id = deviceEntry["Id"];
                device.Name = deviceEntry["Name"];

                deviceList.Add(device);
            }

            return deviceList;
        }

        // Method assumes the credentials are validated, return status code
        public static async Task<HttpStatusCode> RequestLogin(string username, string password)
        {
            Dictionary<string, string> credentials = new Dictionary<string, string>();
            credentials["Username"] = username;
            credentials["Password"] = password;

            string serializedData = JsonConvert.SerializeObject(credentials);
            StringContent content = new StringContent(serializedData, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await httpClient.PostAsync("login", content);

            if (!response.IsSuccessStatusCode)
            {
                return response.StatusCode;
            }

            var cookieEnumerator = clientHandler.CookieContainer.GetCookies(httpClient.BaseAddress).GetEnumerator();
            bool result = cookieEnumerator.MoveNext();

            if (result)
            {
                var cookie = (Cookie)cookieEnumerator.Current;

                Application.Current.Properties.Add("name", cookie.Name);
                Application.Current.Properties.Add("value", cookie.Value);
                Application.Current.Properties.Add("path", cookie.Path);
                Application.Current.Properties.Add("domain", cookie.Domain);
            }

            return response.StatusCode;
        }

        public static async Task<UserModel> GetUserData()
        {
            object cookieName = null;
            object cookieValue = null;
            object cookiePath = null;
            object cookieDomain = null;

            bool result = Application.Current.Properties.TryGetValue("name", out cookieName) &&
                Application.Current.Properties.TryGetValue("value", out cookieValue) &&
                Application.Current.Properties.TryGetValue("path", out cookiePath) &&
                Application.Current.Properties.TryGetValue("domain", out cookieDomain);

            if (!result)
            {
                return null;
            }

            clientHandler.CookieContainer.Add(
                new Cookie(
                    (string)cookieName, 
                    (string)cookieValue, 
                    (string)cookiePath, 
                    (string)cookieDomain));
            HttpResponseMessage response = await httpClient.GetAsync("user");

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            var serializedData = await response.Content.ReadAsStringAsync();
            UserModel responseData = JsonConvert.DeserializeObject<UserModel>(serializedData);

            return responseData;
        }

        public static async Task<object> GetAsync(string path)
        {
            HttpResponseMessage response = await httpClient.GetAsync(path);

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            var serializedData = await response.Content.ReadAsStringAsync();
            dynamic responseData = JsonConvert.DeserializeObject(serializedData);
            return responseData;
        }

        public static async void PutAsync(string path)
        {
            await httpClient.PutAsync(path, null);
        }
    }
}
