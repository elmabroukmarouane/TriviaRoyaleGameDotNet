using TriviaRoyaleGame.Business.Services.SendEmails.Models.Classes;

namespace TriviaRoyaleGame.Business.Services.SendEmails.Interface;
public interface ISendMailService
{
    Task<string?> Send(EmailMessage? emailMessage);
}
