using AustPIC.Models;
using Microsoft.AspNetCore.Mvc;

namespace AustPICWeb.Repositories.Newsletter
{
    public interface INewsletterRepository
    {
        Task<dynamic> SaveEmail(NewsletterSubscriber newsletter);
    }
}
