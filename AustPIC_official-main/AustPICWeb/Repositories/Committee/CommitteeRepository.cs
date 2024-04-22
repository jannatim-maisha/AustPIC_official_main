using AustPIC.db.DbOperations;
using AustPIC.Models;
using AustPICWeb.DBContexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AustPICWeb.Repositories.Committee
{
    public class CommitteeRepository : ICommitteeRepository
    {
        private readonly IDapperDBContext _dapperDBContext;

        string GetAllCommitteeSP = "AustPIC_GetAllCommittee";
        string GetAllCommitteeBySemesterSP = "AustPIC_GetAllCommitteeBySemester";
        string GetTopCommitteeSP = "AustPIC_GetTopCommittee";
        string GetAllCommitteeSemesterSP = "AustPIC_GetAllCommitteeSemester";

        public CommitteeRepository(IDapperDBContext dapperDBContext)
        {
            _dapperDBContext = dapperDBContext;
        }

        public async Task<List<CommitteeModel>> GetMemberList()
        {
            try
            {
                var result = await _dapperDBContext.GetInfoListAsync<CommitteeModel>(new
                {
                }, GetAllCommitteeSP);
                List<CommitteeModel> committeeList = (List<CommitteeModel>)result;
                return committeeList;
            }
            catch (Exception ex)
            {
                return new List<CommitteeModel> { new CommitteeModel() };
            }
        }
        public async Task<List<CommitteeModel>> GetTopMemberList()
        {
            try
            {
                var result = await _dapperDBContext.GetInfoListAsync<CommitteeModel>(new
                {
                }, GetTopCommitteeSP);
                List<CommitteeModel> committeeList = (List<CommitteeModel>)result;
                return committeeList;
            }
            catch (Exception ex)
            {
                return new List<CommitteeModel> { new CommitteeModel() };
            }
        }

        public async Task<List<String>> CommitteeSemesterList()
        {
            Console.WriteLine("Semester DB accessed");
            try
            {
                var result = await _dapperDBContext.GetInfoListAsync<String>(new
                {
                }, GetAllCommitteeSemesterSP);
                List<String> committeeList = (List<String>)result;
                return committeeList;
            }
            catch (Exception ex)
            {
                return new List<String>();
            }
        }

        public async Task<List<CommitteeModel>> GetMemberListBySemester(String semester)
        {
            try
            {
                var result = await _dapperDBContext.GetInfoListAsync<CommitteeModel>(new
                {
                    semester = semester
                }, GetAllCommitteeBySemesterSP);
                List<CommitteeModel> committeeList = (List<CommitteeModel>)result;
                return committeeList;
            }
            catch (Exception ex)
            {
                return new List<CommitteeModel> { new CommitteeModel() };
            }
        }

    }
}

