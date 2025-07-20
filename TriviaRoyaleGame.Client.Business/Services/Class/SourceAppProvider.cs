using TriviaRoyaleGame.Client.Business.Services.Interface;

namespace TriviaRoyaleGame.Client.Business.Services.Class
{
    public class SourceAppProvider(string sourceApp) : ISourceAppProvider
    {
        private readonly string _sourceApp = sourceApp;

        public string GetSourceApp() => _sourceApp;
    }
}
