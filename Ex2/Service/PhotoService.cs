using Ex2.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Ex2.Service
{
    class PhotoService
    {
        private const string ApiBaseUrl = "https://jsonplaceholder.typicode.com";
        private const string ApiSongPath = "https://jsonplaceholder.typicode.com/photos";
        public async Task<List<Photo>> GetLatestPhotoAsync()
        {
            List<Photo> result = new List<Photo>();
            try
            {
                HttpClient httpClient = new HttpClient();
                /*httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {credential.access_token}");*/
                var requestConnection = await httpClient.GetAsync(ApiBaseUrl + ApiSongPath);
                Console.WriteLine(requestConnection.StatusCode);
                if (requestConnection.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var content = await requestConnection.Content.ReadAsStringAsync();
                    result = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Photo>>(content);
                    return result;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return null;
        }
    }
}
