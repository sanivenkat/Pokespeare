using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using Pokespeare.Common;
namespace TestProviders
{
    [TestClass]
    public class UnitTest1
    {
        readonly static string charizardReply=@"Charizard flies around the sky in search of powerful opponents.
It breathes fire of such great heat that it melts anything.
However, it never turns its fiery breath on any opponent
weaker than itself.";
        [TestMethod]
        public async Task  TestPokemonProvider()
        {
            var provider = new PokemonInfoProvider();
            var res = await provider.GetDescriptionAsync("charizard");
            Assert.AreNotEqual(-1,res.IndexOf("Charizard flies"));
        }
        [TestMethod]
        public async Task  TestShakespeareTranslator()
        {
            var translator = new ShakespeareTranslator();
            var res = await translator.TranslateAsync(charizardReply);
            Assert.AreNotEqual(-1,res.IndexOf("flies 'round"));
        }
    }
}
