using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Net.Http;
namespace TestApi
{
    class Program
    {
        static readonly HttpClient client = new HttpClient();
        static readonly string TestUri = "https://localhost:44319/Pokemon/charizard";
        static readonly string Expected = "flies 'round"; //a not so strcit marker we have the expected Shakespeare reply...
        static async Task Main(string[] args)
        {
            var saveColor = Console.ForegroundColor;
            try{
                
                 
                var httpResponse = await client.GetAsync(TestUri);

                if (!httpResponse.IsSuccessStatusCode)
                {
                    await Error($"Failure: Http error code:{httpResponse.StatusCode}");
                }
                else
                {
                    var response = await httpResponse.Content.ReadAsStringAsync();
                    if(response.IndexOf(Expected) == -1 )
                    {
                        await Error($"reply {response} DOES NOT contain expected'{Expected}'");
                    }
                    else
                    {
                        await Info("Test passed.");
                    }
                }
                
            }
            catch(Exception e)
            {
                await Error($"Fatal error:{e.ToString()}");
            }

            Console.ForegroundColor = saveColor;
        }
        static async Task  Error(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            await Console.Error.WriteLineAsync(message);
        }    
        static async Task Info(string message)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            await Console.Error.WriteLineAsync(message);
        }  
              
    }
    
}
