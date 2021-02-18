using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Go_Game_lorleveque_WinForm.Utils
{
    class ApiBotResult
    {
        public int x;
        public int y;
    }
    class ApiCall
    {
        private string baseUrl;

        private HttpClient client;
        private HttpResponseMessage response;

        public ApiCall(string baseUrl)
        {
            this.baseUrl = baseUrl;
            client = new HttpClient();
            setURI();
        }

        public void setBaseUri(string uri)
        {
            baseUrl = uri;
            setURI();
        }
        private void setURI()
        {
            client.BaseAddress = new Uri(baseUrl);
        }

        public void AddAcceptType(string type)
        {
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue(type)
            );
        }

        public void Call()
        {
            response = client.GetAsync("").Result;
        }
        public void CallWithGetParams(string param)
        {
            response = client.GetAsync(param).Result;
        }

        public ApiBotResult GetResultBot()
        {
            if (response.IsSuccessStatusCode)
            {
                var data = response.Content.ReadAsStringAsync();

                return JsonConvert.DeserializeObject<ApiBotResult>(data.Result);
            }
            else
            {
                return null;
            }
        }
    }
}
