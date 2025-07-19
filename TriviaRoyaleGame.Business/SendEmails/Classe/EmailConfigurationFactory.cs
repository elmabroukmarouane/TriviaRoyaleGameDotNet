using TriviaRoyaleGame.Business.SendEmails.Interface;
using TriviaRoyaleGame.Business.Services.SendEmails.Models.Classes;

namespace TriviaRoyaleGame.Business.SendEmails.Classe
{
    public class EmailConfigurationFactory(EmailConfiguration emailConfiguration) : IEmailConfigurationFactory
    {
        #region ATTRIBUTES
        protected readonly EmailConfiguration _emailConfiguration = emailConfiguration ?? throw new ArgumentException(null, nameof(emailConfiguration));
        #endregion

        #region METHODS
        public EmailConfiguration GetEmailConfiguration() => _emailConfiguration;
        #endregion
    }
}
