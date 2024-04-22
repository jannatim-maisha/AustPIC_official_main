using AustPIC.Models;

namespace AustPICWeb.Repositories.CssVariable
{
    public interface ICssRepository
    {
        Task<List<CssVariableModel>> GetCssVariablesList();
    }
}
