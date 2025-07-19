using TriviaRoyaleGame.Business.SendEmails.Classe;
using TriviaRoyaleGame.Business.SendEmails.Interface;
using TriviaRoyaleGame.Business.Services.SendEmails.Interface;
using TriviaRoyaleGame.Business.Services.SendEmails.Models.Classes;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using MimeKit.Text;
using System.Threading;

namespace TriviaRoyaleGame.Business.Services.SendEmails.Classe
{
    public class SendMailService(ISmtpClient smtpClient, IEmailConfigurationFactory emailConfigurationFactory) : ISendMailService
    {
        #region ATTRIBUTES
        private readonly ISmtpClient _smtpClient = smtpClient ?? throw new ArgumentException(nameof(smtpClient));
        private readonly IEmailConfigurationFactory _emailConfigurationFactory = emailConfigurationFactory ?? throw new ArgumentException(nameof(emailConfigurationFactory));
        #endregion

        #region METHODS
        public async Task<string?> Send(EmailMessage? emailMessage)
        {
            if (emailMessage is not null)
            {
                if (emailMessage.ToAddresses is not null && emailMessage.ToAddresses.Any() && emailMessage.FromAddresses is not null && emailMessage.FromAddresses.Any())
                {
                    var message = new MimeMessage();
                    message.To.AddRange(emailMessage.ToAddresses?.Select(x => new MailboxAddress(x.Name, x.Address)));
                    message.From.AddRange(emailMessage.FromAddresses?.Select(x => new MailboxAddress(x.Name, x.Address)));
                    message.Subject = emailMessage.Subject;
                    var textPart = new TextPart(TextFormat.Html)
                    {
                        Text = emailMessage.Content
                    };
                    var multipart = new Multipart()
                    {
                        textPart
                    };
                    if (emailMessage.FilesPath != null)
                    {
                        if (emailMessage.FilesPath.Any())
                        {
                            foreach (var path in emailMessage.FilesPath)
                            {
                                if (!string.IsNullOrEmpty(path))
                                {
                                    var attachment = new MimePart()
                                    {
                                        Content = new MimeContent(File.OpenRead(path), ContentEncoding.Default),
                                        ContentDisposition = new ContentDisposition(ContentDisposition.Attachment),
                                        ContentTransferEncoding = ContentEncoding.Base64,
                                        FileName = Path.GetFileName(path)
                                    };
                                    multipart.Add(attachment);
                                }
                            }
                        }
                    }
                    message.Body = multipart;
                    _smtpClient.ServerCertificateValidationCallback = (s, c, h, e) => true;

                    var emailConfiguration = _emailConfigurationFactory.GetEmailConfiguration();

                    if(emailConfiguration.SmtpProvider == SmtpProvider.GMAIL)
                    {
                        if (_emailConfigurationFactory is null || emailConfiguration.SmtpServer is null || emailConfiguration.SmtpPort <= 0 || emailConfiguration.SmtpUsername is null || emailConfiguration.SmtpPassword is null) return null;
                        await _smtpClient.ConnectAsync(emailConfiguration.SmtpServer, emailConfiguration.SmtpPort, SecureSocketOptions.Auto);
                        await _smtpClient.AuthenticateAsync(emailConfiguration.SmtpUsername, emailConfiguration.SmtpPassword);
                    }
                    else
                    {
                        if (_emailConfigurationFactory is null || emailConfiguration.SmtpServer is null || emailConfiguration.SmtpPort <= 0) return null;
                        await _smtpClient.ConnectAsync(emailConfiguration.SmtpServer, emailConfiguration.SmtpPort, SecureSocketOptions.Auto);
                        if(emailConfiguration.SmtpProvider != SmtpProvider.MAILHOG)
                        {
                            await _smtpClient.AuthenticateAsync(emailConfiguration.SmtpUsername, emailConfiguration.SmtpPassword);
                        }
                    }

                    var response = await _smtpClient.SendAsync(message);
                    await _smtpClient.DisconnectAsync(true);
                    return response;
                }
            }
            return null;
        }
        #endregion
    }
}
