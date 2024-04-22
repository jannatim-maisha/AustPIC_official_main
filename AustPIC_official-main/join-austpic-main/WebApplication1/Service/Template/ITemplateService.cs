namespace ClubWebsite.Service.Template
{
    public interface ITemplateService
    {
        Task<string> GetTemplateBody(int TemplateID, string ClubID, string InvoiceNo);
    }
}
