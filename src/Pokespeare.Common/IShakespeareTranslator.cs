using System.Threading.Tasks;
public interface IShakespeareTranslator
{
    Task<string> TranslateAsync(string source);
}