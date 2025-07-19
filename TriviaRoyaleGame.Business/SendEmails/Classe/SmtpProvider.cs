namespace TriviaRoyaleGame.Business.SendEmails.Classe
{
    /// <summary>
    /// This is a class enum for checking the config in appsetting.json files to match which SMTP we are using in our application
    /// </summary>
    public class SmtpProvider
    {
        /// <summary>
        /// This MAILHOG is a SMTP for test development
        /// </summary>
        public const string MAILHOG = "MAILHOG";
        /// <summary>
        /// This is the GMAIL SMTP
        /// </summary>
        public const string GMAIL = "GMAIL";
    }
}
