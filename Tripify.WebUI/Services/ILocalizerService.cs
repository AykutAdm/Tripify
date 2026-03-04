namespace Tripify.WebUI.Services
{
    public interface ILocalizerService
    {
        string this[string key] { get; }
    }
}
