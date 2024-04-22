namespace ClubWebsite.Models.Payment
{
    public class BkashGrantTokenModel
    {
        public string id_token { get; set; }
        public string token_type { get; set; }
        public int ? expires_in { get; set; }
        public string refresh_token { get; set; }
    }
}
