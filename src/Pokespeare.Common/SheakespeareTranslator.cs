using System;
using System.Threading.Tasks;
using System.Net.Http;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace Pokespeare.Common
{
    public class ShakespeareTranslator : IShakespeareTranslator
    {
        static readonly HttpClient client = new HttpClient();
        static readonly string baseUri = "https://api.funtranslations.com/translate/shakespeare.json";
        public async Task<string> TranslateAsync(string source)
        {
            var content = new FormUrlEncodedContent(new [] {new KeyValuePair<string,string>("text",source)});
            var httpResponse = await client.PostAsync(baseUri,content);

            if (!httpResponse.IsSuccessStatusCode)
            {
                throw new RequestException(httpResponse.StatusCode,$"Http error from server:{httpResponse.StatusCode}");
            }
            else
            {
                 dynamic obj = JValue.Parse( await httpResponse.Content.ReadAsStringAsync() );
                 if(obj.success.total!=0)
                 {
                     return obj.contents.translated;
                 }
                 else
                 {
                     throw new Exception("Translation unavailable");
                 }
            }
        }
    }
}
