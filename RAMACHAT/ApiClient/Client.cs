using Newtonsoft.Json;
using RAMACHAT.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace RAMACHAT.ApiClient
{
    public class Client
    {
        private string TOKEN = null;
        public string USERID;

        public string getToken()
        {
            return this.TOKEN;
        }
        public void setToken(string tok)
        {
            TOKEN = tok;
        }
        public async Task<string> PostAsync(string uri, string data)
        {
            var httpClient = new HttpClient();
            if (this.TOKEN != null)
            {
                addHeader(httpClient, this.TOKEN);
            }{ }
            var response = await httpClient.PostAsync(uri, new StringContent(data, Encoding.UTF8, "application/json"));
            response.EnsureSuccessStatusCode();
            string content = await response.Content.ReadAsStringAsync();
            Debug.WriteLine(content);
            return content;
        }
        public async Task<string> GetAsync(string uri)
        {
            var httpClient = new HttpClient();
            if (this.TOKEN != null)
            {
                addHeader(httpClient, this.TOKEN);
            }{ }
            var response = await httpClient.GetAsync(uri);
            response.EnsureSuccessStatusCode();
            string content = await response.Content.ReadAsStringAsync();
            return content;
        }
        public async Task<string> Login(string data)
        {
            return await this.PostAsync(ApiLink.getLoginLink(), data);
        }
        public async Task<string> getUserById(string id)
        {
            return await this.GetAsync(ApiLink.getUserLink() + id);
        }
        public async Task<string> getUserUnreadMessages()
        {
            return await this.GetAsync(ApiLink.getAllUserLink());
        }
        public async void getRoomMessagesByUserId(string id)
        {
            Debug.WriteLine(ApiLink.getRoomMessageByUserIdLink() + id);
            App.ViewModel.Items.Clear();
            var result = await GetAsync(ApiLink.getRoomMessageByUserIdLink() + id);
            MessageObject resultMessageObject = JsonConvert.DeserializeObject<MessageObject>(result);
            foreach (var message in resultMessageObject.data)
            {
                App.ViewModel.Items.Add(new ViewModels.ItemViewModel() { Avatar = new Uri(message._userId.avatar), CreateAt = message.createdAt, MessageText = message.message, SenderID = message._userId._id });
            }
        }
        public async Task<string> getAllFriends()
        {
            return await GetAsync(ApiLink.getAllUserLink());
        }
        public void addHeader(HttpClient httpClient, string token)
        {
            httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
        }
        public async Task<bool> SignUp(string data)
        {
            string result = await this.PostAsync(ApiLink.getSignUpLink(), data.ToString());
            if (result.Contains("Successfully"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
