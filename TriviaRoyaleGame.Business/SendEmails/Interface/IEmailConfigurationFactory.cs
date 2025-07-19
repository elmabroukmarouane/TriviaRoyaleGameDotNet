using TriviaRoyaleGame.Business.Services.SendEmails.Models.Classes;

namespace TriviaRoyaleGame.Business.SendEmails.Interface
{
    public interface IEmailConfigurationFactory
    {
        EmailConfiguration GetEmailConfiguration();
    }
}
