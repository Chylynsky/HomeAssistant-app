using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using HomeAssistant.Model;
using HomeAssistant.Model.Devices;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace HomeAssistant.Helper
{
    public static class HomeAssistantHttpClient
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

        static HomeAssistantHttpClient()
        {
            clientHandler = new HttpClientHandler();
            Proxy = new WebProxy(HomeAssistant.Properties.Resources.Proxy);

            httpClient = new HttpClient(clientHandler);
            Address = new Uri("http://home.as");
            httpClient.Timeout = TimeSpan.FromSeconds(5.0);
        }

        private static void DumpSessionCookie()
        {
            var cookieEnumerator = clientHandler.CookieContainer.GetCookies(httpClient.BaseAddress).GetEnumerator();
            bool result = cookieEnumerator.MoveNext();

            if (result)
            {
                var cookie = (Cookie)cookieEnumerator.Current;

                Application.Current.Properties["name"] = cookie.Name;
                Application.Current.Properties["value"] = cookie.Value;
                Application.Current.Properties["path"] = cookie.Path;
                Application.Current.Properties["domain"] = cookie.Domain;
            }
        }

        public static async Task<List<IDeviceModel>> GetConnectedDevices()
        {
            var deviceList = new List<IDeviceModel>();

            HttpResponseMessage response = await httpClient.GetAsync("devices", HttpCompletionOption.ResponseContentRead);

            if (!response.IsSuccessStatusCode)
            {
                return deviceList;
            }

            string responseContent = await response.Content.ReadAsStringAsync();
            var devicesList = JsonConvert.DeserializeObject<List<Dictionary<string, string>>>(responseContent);

            foreach (var deviceEntry in devicesList)
            {
                var device = DeviceLinker.GetDeviceModelForTypeName(deviceEntry["Type"]);

                device.Id = deviceEntry["Id"];
                device.Name = deviceEntry["Name"];

                deviceList.Add(device);
            }

            DumpSessionCookie();

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
            HttpResponseMessage response = null;

            try
            {
                response = await httpClient.PostAsync("login", content);
            }
            catch (TaskCanceledException)
            {
                return HttpStatusCode.ServiceUnavailable;
            }

            if (!response.IsSuccessStatusCode)
            {
                return response.StatusCode;
            }

            DumpSessionCookie();

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

            HttpResponseMessage response = null;

            try
            {
                response = await httpClient.GetAsync("user");
            }
            catch (TaskCanceledException)
            {
                return null;
            }

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            var serializedData = await response.Content.ReadAsStringAsync();
            UserModel responseData = JsonConvert.DeserializeObject<UserModel>(serializedData);

            DumpSessionCookie();

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

            DumpSessionCookie();

            return responseData;
        }

        public static async Task PutAsync(string path)
        {
            await httpClient.PutAsync(path, null);
            DumpSessionCookie();
        }

        public static async Task CreateRoomAsync(RoomType type, string name)
        {
            var roomData = new Dictionary<string, string>();

            roomData["Type"] = type.ToString();
            roomData["Name"] = name;
            roomData["Devices"] = null;

            string serializedData = JsonConvert.SerializeObject(roomData);
            StringContent content = new StringContent(serializedData, Encoding.UTF8, "application/json");
            HttpResponseMessage response = null;

            try
            {
                response = await httpClient.PutAsync("/create_room", content);
            }
            catch (TaskCanceledException)
            {
                return;
            }

            if (!response.IsSuccessStatusCode)
            {
                return;
            }

            DumpSessionCookie();
        }
    }
}
