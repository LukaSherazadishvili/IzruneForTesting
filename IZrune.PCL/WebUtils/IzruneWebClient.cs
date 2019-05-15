using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace IZrune.PCL.WebUtils
{
   public sealed class IzruneWebClient
    {

        private static IzruneWebClient instance = null;
        private static readonly object padlock = new object();

        IzruneWebClient()
        {
        }

        public static IzruneWebClient Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new IzruneWebClient();
                    }
                    return instance;
                }
            }
        }

        HttpClient httpClient;

        public async Task<HttpResponseMessage>GetPostData(string url, FormUrlEncodedContent content)
        {
            httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            httpClient.DefaultRequestHeaders.TryAddWithoutValidation("ContentType", "application/json");
            var response = await httpClient.PostAsync(url, content);
            return response;
        }


        public async Task<T>GetDataAsync<T>(string uri)
        {
            try
            {
                httpClient = new HttpClient();
                httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                httpClient.DefaultRequestHeaders.TryAddWithoutValidation("ContentType", "application/json");
                var response = await httpClient.GetStringAsync(uri);
                return JsonConvert.DeserializeObject<T>(response);
            }
            catch(Exception ex)
            {
                return default(T);
            }
        }
    }
}
