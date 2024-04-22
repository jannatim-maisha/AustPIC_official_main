namespace ClubWebsite.Models.Payment
{
    public class BkashCredentials
    {
        public string UserName { get; set; }

        public string Password { get; set; }

        public string AppKey { get; set; }

        public string AppSecret { get; set; }

        public string Token { get; set; }

        public string RefreshToken { get; set; }

        public DateTime? CreatedOn { get; set; }

        public decimal? ValiditySeconds { get; set; }

    }
}
