using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Pokespeare.Common;
using System.Net;

namespace Pokespeare.Controllers
{
    [Route("/[controller]")]
    [ApiController]
    public class PokemonController : ControllerBase
    {
        readonly ILogger<PokemonController> logger;
        readonly IPokemonInfoProvider provider;
        readonly IShakespeareTranslator translator;
        public PokemonController(ILogger<PokemonController> logger,IPokemonInfoProvider provider,IShakespeareTranslator translator)
        {
            this.logger = logger;
            this.provider = provider;
            this.translator = translator;
        }
       
        // GET /pokemon/name
        [HttpGet("{pokemon}")]
        public async Task<IActionResult> Get(string pokemon)
        {
            try{
                logger.LogInformation("Retrieving Pokemon informations");
                var description =  await provider.GetDescriptionAsync(pokemon);
                logger.LogDebug($"Pokemon original description:{description}");
                var translated = await translator.TranslateAsync(description);
                return Ok(new {name=pokemon,description=translated});
            }
            catch(RequestException re)
            {
                if(re.Code == HttpStatusCode.NotFound)
                    return NotFound();
                else
                    return BadRequest(re.Message);
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
