using TriviaRoyaleGame.Client.Business.Providers.Interfaces;

namespace TriviaRoyaleGame.Client.Business.Providers.Classes
{
    public class SourceAppProvider(string sourceApp) : ISourceAppProvider
    {
        private readonly string _sourceApp = sourceApp;

        public string GetSourceApp() => _sourceApp;
    }
}
