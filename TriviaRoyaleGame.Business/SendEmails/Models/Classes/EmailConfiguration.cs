﻿namespace TriviaRoyaleGame.Business.Services.SendEmails.Models.Classes;
public class EmailConfiguration
{
    public string? SmtpServer { get; set; }
    public int SmtpPort { get; set; }
    public string? SmtpUsername { get; set; }
    public string? SmtpPassword { get; set; }
    public string? SmtpProvider { get; set; }
}