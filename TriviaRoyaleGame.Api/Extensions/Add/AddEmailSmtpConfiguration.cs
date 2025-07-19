using TriviaRoyaleGame.Business.SendEmails.Classe;
using TriviaRoyaleGame.Business.SendEmails.Interface;
using TriviaRoyaleGame.Business.Services.SendEmails.Models.Classes;

namespace TriviaRoyaleGame.Api.Extensions.Add;
public static class AddEmailSmtpConfiguration
{
    public static void AddEmailSmtpConfigurationExtension(this IServiceCollection self, IConfiguration configuration)
    {
        var smtpServer = configuration.GetSection("EmailConfiguration:SmtpServer").Value;
        var smtpPort = int.Parse(configuration.GetSection("EmailConfiguration:SmtpPort").Value!);
        var smtpUsername = configuration.GetSection("EmailConfiguration:SmtpUsername").Value;
        var smtpPassword = configuration.GetSection("EmailConfiguration:SmtpPassword").Value;
        var smtpProvider = configuration.GetSection("EmailConfiguration:SmtpProvider").Value;

        var emailSmtpConfiguration = new EmailConfiguration()
        {
            SmtpServer = smtpServer,
            SmtpPort = smtpPort,
            SmtpUsername = smtpUsername,
            SmtpPassword = smtpPassword,
            SmtpProvider = smtpProvider
        };
        self.AddSingleton<IEmailConfigurationFactory>(new EmailConfigurationFactory(emailSmtpConfiguration));
    }
}
