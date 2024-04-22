using AustPICWeb.Data;
using AustPICWeb.Repositories.Committee;
using Microsoft.AspNetCore.Mvc;

namespace AustPICWeb.ViewComponents
{
    [ViewComponent(Name = "CommitteeSemester")]
    public class CommitteeSemesterViewComponent : ViewComponent
    {
        private readonly ICommitteeRepository _testService;
        public CommitteeSemesterViewComponent(ICommitteeRepository testService)
        {
            _testService = testService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var semester = await _testService.CommitteeSemesterList();
            return View("Index", semester);
        }
    }
}
