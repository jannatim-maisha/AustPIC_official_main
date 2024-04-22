using ClubWebsite.Models.Common;
using ClubWebsite.Models.Join;
using ClubWebsite.Models.Payment;
using Microsoft.AspNetCore.Mvc;

namespace ClubWebsite.Service.Join
{
    public interface IJoinService
    {
        Task<JsonResult> CheckMemberInformationNew(MemberViewModel member);
        Task<string> GenerateTokenForMember();
        Task<List<Department>> GetDepartmentList();
        Task<List<MemberModel>> GetMemberList();
        Task<List<Semester>> GetSemesterList();
        Task<List<MemberToken>> GetTokenForMember(string Token);
        Task<string> InsertNewMember(MemberModel member);
        //Task<JsonResult> InsertNewMember(MemberViewModel member, BkashExecutePaymentReturnModel executeReturnModel);
        Task<JsonResult> InsertNewMemberFromTemp(BkashExecutePaymentReturnModel executeReturnModel, string Token);
        //Task<string> InsertNewMemberInterest(MemberInterest interest);
        Task<JsonResult> InsertNewMemberTemp(MemberViewModel member, string Token);
        Task<bool> VerifyTokenForMember(string Token);
    }
}
