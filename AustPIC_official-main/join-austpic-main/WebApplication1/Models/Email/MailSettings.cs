﻿namespace ClubWebsite.Models.Email
{
    public class MailSettings
    {
        public string FromEmail { get; set; }
        public string ReplyTo { get; set; }
        public string DisplayName { get; set; }
        public string Password { get; set; }
        public string Host { get; set; }
        public int Port { get; set; }
    }
}
