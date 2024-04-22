using AustPIC.db;
using AustPIC.Models;
using AustPICWeb.DBContexts;
using Microsoft.AspNetCore.Mvc;

namespace AustPICWeb.Repositories.Newsletter
{
    public class NewsletterRepository : INewsletterRepository
    {
        private readonly IDapperDBContext _dapperDBContext;

        string SaveEmailSP = "AustPIC_SaveEmail";

        public NewsletterRepository(IDapperDBContext dapperDBContext)
        {
            _dapperDBContext = dapperDBContext;
        }

        public async Task<dynamic> SaveEmail(NewsletterSubscriber newsletter)
        {
            try
            {
                var result = await _dapperDBContext.GetInfoAsync<dynamic>(new
                {
                    email = newsletter.email,
                    subscribed_at = DateTime.UtcNow.AddHours(6)
                }, SaveEmailSP);

                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while saving the email to the database.", ex);            
            }
        }
    }
}
