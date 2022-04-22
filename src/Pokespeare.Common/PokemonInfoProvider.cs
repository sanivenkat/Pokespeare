using System;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json.Linq;
using System.Linq;

namespace Pokespeare.Common
{
    public class PokemonInfoProvider : IPokemonInfoProvider
    {
        static readonly string baseUri = "https://pokeapi.co/api/v2/pokemon-species/{0}";
        static readonly HttpClient client = new HttpClient();
        public async Task<string> GetDescriptionAsync(string name)
        {
           
            var uri = string.Format(baseUri,name);
            var httpResponse = await client.GetAsync(uri);

            if (!httpResponse.IsSuccessStatusCode)
            {
                throw new RequestException(httpResponse.StatusCode,$"Http error from server:{httpResponse.StatusCode}");
            }
            else
            {
                dynamic obj = JValue.Parse(await httpResponse.Content.ReadAsStringAsync());
                JArray entries = obj.flavor_text_entries;
                foreach( var token in entries )
                {
                    dynamic d = token;
                    if(d.language.name=="en")
                    {
                        return d.flavor_text;
                    }
                }
            }
            return  "";
           
        }
    }
}