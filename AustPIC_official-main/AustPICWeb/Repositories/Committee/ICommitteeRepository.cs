using AustPIC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AustPICWeb.Repositories.Committee
{
    public interface ICommitteeRepository
    {
        Task<List<CommitteeModel>> GetMemberList();
        Task<List<CommitteeModel>> GetTopMemberList();
        Task<List<String>> CommitteeSemesterList();
        Task<List<CommitteeModel>> GetMemberListBySemester(String semester);
    }
}
