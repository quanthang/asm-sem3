using form.Entities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace form.Service
{
    public class SongService
    {
        private const string ApiBaseUrl = "http://music-i-like.herokuapp.com";
        private const string ApiSongPath = "api/v1/songs";
        public async Task<List<Song>> GetLatestSongAsync()
        {
            List<Song> result = new List<Song>();
            try
            {
                HttpClient httpClient = new HttpClient();
                /*httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {credential.access_token}");*/
                var requestConnection = await httpClient.GetAsync(ApiBaseUrl + ApiSongPath);
                Console.WriteLine(requestConnection.StatusCode);
                if (requestConnection.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var content = await requestConnection.Content.ReadAsStringAsync();
                    result = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Song>>(content);
                    return result;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return null;
        }

        public async Task<bool> CreateSongAsync(Song song)
        {
            try
            {
                var accountJson = Newtonsoft.Json.JsonConvert.SerializeObject(song);
                HttpClient httpClient = new HttpClient();
                Debug.WriteLine(accountJson);
                var httpContent = new StringContent(accountJson, Encoding.UTF8, "application/json");
                var requestConnection = await httpClient.PostAsync("http://music-i-like.herokuapp.com/api/v1/song/mine", httpContent);

                if (requestConnection.StatusCode == System.Net.HttpStatusCode.Created)
                {
                    return true;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return false;
        }
    }
}
