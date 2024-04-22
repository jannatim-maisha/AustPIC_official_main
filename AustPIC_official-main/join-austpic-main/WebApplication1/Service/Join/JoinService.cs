using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using ClubWebsite.DBContexts;
using ClubWebsite.Models.Common;
using System.Collections.Generic;
using ClubWebsite.Models.Join;
using ClubWebsite.Models.Payment;
using ClubWebsite.Models.Sms;
using Microsoft.AspNetCore.Mvc;
using ClubWebsite.Service.Payment;
using ClubWebsite.Service.Sms;
using ClubWebsite.Service.Email;
using AutoMapper;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Http;
using ClubWebsite.Service.Email;
using ClubWebsite.Models.Email;
using ClubWebsite.Service.Template;
using Microsoft.AspNetCore.Hosting.Server;
using AngleSharp.Html.Dom;

namespace ClubWebsite.Service.Join
{
    public class JoinService : IJoinService
    {
        #region Fields

        private readonly IDapperDBContext _dapperDBContext;
        private readonly IConfiguration _configuration;
        private readonly ISmsService _smsService;
        private readonly IPaymentService _paymentService;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IEmailService _emailService;
        private readonly ITemplateService _templateService;


        String GetAllMemberSP = "ClubWebsite_GetAllMember";
        String GetAllMemberTempSP = "ClubWebsite_GetAllMemberTemp";
        String GetAllDepartmentSP = "ClubWebsite_GetAllDepartment";
        String GetAllSemesterSP = "ClubWebsite_GetAllSemester";
        String InsertNewMemberSP = "ClubWebsite_InsertNewMember";
        String InsertNewMemberInterestSP = "ClubWebsite_InsertNewMemberInterest";
        String InsertNewMemberTempSP = "ClubWebsite_InsertNewMemberTemp";
        String InsertNewMemberInterestTempSP = "ClubWebsite_InsertNewMemberInterestTemp";
        String GenerateTokenForMemberSP = "ClubWebsite_GenerateTokenForMember";
        String VerifyTokenForMemberSP = "ClubWebsite_VerifyTokenForMember";
        String GetTokenForMemberSP = "ClubWebsite_GetTokenForMember";
        String ConvertMemberFromTempSP = "ClubWebsite_ConvertMemberFromTemp";
        #endregion

        #region Ctor

        public JoinService(IDapperDBContext dapperDBContext,IConfiguration configuration,IPaymentService paymentService,ISmsService smsService,IMapper mapper,IWebHostEnvironment webHostEnvironment,IEmailService emailService,ITemplateService templateService)
        {
            _dapperDBContext = dapperDBContext;
            _configuration = configuration;
            _smsService = smsService;
            _paymentService = paymentService;
            _mapper = mapper;
            _webHostEnvironment = webHostEnvironment;
            _emailService = emailService;
            _templateService = templateService;
        }

        #endregion

        #region Methods

        public async Task<List<MemberModel>> GetMemberList()
        {
            try
            {
                var result = await _dapperDBContext.GetInfoListAsync<MemberModel>(new
                {
                }, GetAllMemberSP);
                List<MemberModel> memberList = (List<MemberModel>)result;
                return memberList;
            }
            catch (Exception ex)
            {
                return new List<MemberModel> { new MemberModel() };
            }
        }
        public async Task<List<MemberModelTemp>> GetMemberListTemp()
        {
            try
            {
                var result = await _dapperDBContext.GetInfoListAsync<MemberModelTemp>(new
                {
                }, GetAllMemberTempSP);
                List<MemberModelTemp> memberList = (List<MemberModelTemp>)result;
                return memberList;
            }
            catch (Exception ex)
            {
                return new List<MemberModelTemp> { new MemberModelTemp() };
            }
        }
        public async Task<List<Department>> GetDepartmentList()
        {
            try
            {
                var result = await _dapperDBContext.GetInfoListAsync<Department>(new
                {
                }, GetAllDepartmentSP);
                List<Department> departmentList = (List<Department>)result;
                return departmentList;
            }
            catch(Exception ex)
            {
                return new List<Department> { new Department() };
            }
        }
        public async Task<List<Semester>> GetSemesterList()
        {
            try
            {
                var result = await _dapperDBContext.GetInfoListAsync<Semester>(new
                {
                }, GetAllSemesterSP);
                List<Semester> semesterList = (List<Semester>)result;
                return semesterList;
            }
            catch (Exception ex)
            {
                return new List<Semester> { new Semester() };
            }
        }
        public async Task<string> InsertNewMember(MemberModel member)
        {
            try
            {
                var result = await _dapperDBContext.GetInfoAsync<string>(new
                {
                    Name = member.Name,
                    StudentId = member.StudentId,
                    Email = member.Email,
                    Department = member.Department,
                    Semester = member.Semester,
                    MobileNo = member.MobileNo,
                    BirthDate = member.BirthDate,
                    BloodGroup = member.BloodGroup,
                    Picture = member.Picture,
                    JoinTime = DateTime.UtcNow.AddHours(6)
                }, InsertNewMemberSP);
                return result;
            }
            catch (Exception ex)
            {
                return "";
            }
        }
        private async Task<string> InsertNewMemberTemp(MemberModelTemp member)
        {
            try
            {
                var result = await _dapperDBContext.GetInfoAsync<string>(new
                {
                    Token = member.Token,
                    Name = member.Name,
                    StudentId = member.StudentId,
                    Email = member.Email,
                    Department = member.Department,
                    Semester = member.Semester,
                    MobileNo = member.MobileNo,
                    BirthDate = member.BirthDate,
                    BloodGroup = member.BloodGroup,
                    Picture = member.Token + member.Picture,
                    JoinTime = DateTime.UtcNow.AddHours(6)
                }, InsertNewMemberTempSP);
                return result;
            }
            catch (Exception ex)
            {
                return "";
            }
        }
        private async Task<string> InsertNewMemberInterest(MemberInterest interest)
        {
            try
            {
                var result = await _dapperDBContext.GetInfoAsync<string>(new
                {
                    ClubId = interest.ClubId,
                    IsCompetitiveProgramming = interest.IsCompetitiveProgramming,
                    IsSoftwareDevelopment = interest.IsSoftwareDevelopment,
                    IsWebDevelopment = interest.IsWebDevelopment,
                    IsMobileAppDevelopment = interest.IsMobileAppDevelopment,
                    IsEventManagement = interest.IsEventManagement,
                    IsGraphicsDesign = interest.IsGraphicsDesign,
                    IsPhotography = interest.IsPhotography,
                    IsRobotics = interest.IsRobotics,
                    IsArtificialIntelligence = interest.IsArtificialIntelligence

                }, InsertNewMemberInterestSP);
                return result;
            }
            catch (Exception ex)
            {
                return "";
            }
        }
        private async Task<string> InsertNewMemberInterestTemp(MemberInterestTemp interest)
        {
            try
            {
                var result = await _dapperDBContext.GetInfoAsync<string>(new
                {
                    Token = interest.Token,
                    IsCompetitiveProgramming = interest.IsCompetitiveProgramming,
                    IsSoftwareDevelopment = interest.IsSoftwareDevelopment,
                    IsWebDevelopment = interest.IsWebDevelopment,
                    IsMobileAppDevelopment = interest.IsMobileAppDevelopment,
                    IsEventManagement = interest.IsEventManagement,
                    IsGraphicsDesign = interest.IsGraphicsDesign,
                    IsPhotography = interest.IsPhotography,
                    IsRobotics = interest.IsRobotics,
                    IsArtificialIntelligence = interest.IsArtificialIntelligence

                }, InsertNewMemberInterestTempSP);
                return result;
            }
            catch (Exception ex)
            {
                return "";
            }
        }
        private SendSMSInitModel PrepareConfirmationSMSGM(MemberModel model)
        {
            SendSMSInitModel smsModel = new SendSMSInitModel();
            smsModel.Is_Unicode = false;
            smsModel.Is_Flash = false;
            smsModel.SecretKey = _configuration.GetSection("SMSQ")["SecretKey"];
            smsModel.Message = "Congratulations " + model.Name + "!Your payment is successful.You are now a permanent member of AUST Programming and Informatics Club.\nLearn();Code();Conquer();\n-AUSTPIC";
            smsModel.MobileNumbers = "88" + model.MobileNo;
            return smsModel;
        }
        private async Task<SendEmailInitModel> PrepareConfirmationEmailGM(MemberModel model)
        {
            SendEmailInitModel emailModel = new SendEmailInitModel();
            if(model==null)
            {
                return emailModel;
            }
            emailModel.FromEmail = _configuration.GetSection("MailSettings")["FromEmail"];
            emailModel.Body = await _templateService.GetTemplateBody(1, model.ClubId, "");
            emailModel.ReplyTo = _configuration.GetSection("MailSettings")["ReplyTo"];
            emailModel.Subject = "Registration Confirmation for AUST Programming and Informatics Club (AUSTPIC)";
            emailModel.ToEmails = new List<string>();
            emailModel.ToEmails.Add(model.Email);
            emailModel.PassWord = _configuration.GetSection("MailSettings")["Password"];
            emailModel.SendDate = DateTime.UtcNow.AddHours(6);
            emailModel.Attachments = new List<IFormFile>();
            emailModel.Bcc = new List<string>();
            emailModel.Cc = new List<string>();

            return emailModel;
        }
        private async Task<SendEmailInitModel> PrepareInvoiceEmailGM(string InvoiceNo,string email)
        {
            SendEmailInitModel emailModel = new SendEmailInitModel();
            if (InvoiceNo == null || email==null)
            {
                return emailModel;
            }
            emailModel.FromEmail = _configuration.GetSection("MailSettings")["FromEmail"];
            emailModel.Body = await _templateService.GetTemplateBody(2, "", InvoiceNo);
            emailModel.ReplyTo = _configuration.GetSection("MailSettings")["ReplyTo"];
            emailModel.Subject = "Invoice Slip for Membership Payment";
            emailModel.ToEmails = new List<string>();
            emailModel.ToEmails.Add(email);
            emailModel.PassWord = _configuration.GetSection("MailSettings")["Password"];
            emailModel.SendDate = DateTime.UtcNow.AddHours(6);
            emailModel.Attachments = new List<IFormFile>();
            emailModel.Bcc = new List<string>();
            emailModel.Cc = new List<string>();

            return emailModel;
        }
        /*public async Task<JsonResult> InsertNewMember(MemberViewModel member, BkashExecutePaymentReturnModel executeReturnModel) //unused now
        {
            //unused now
            JsonResult json = await CheckMemberInformationNew(member);
            if (json.Value.ToString().Contains("status = False"))
            {
                return new JsonResult(new
                {
                    status = false,
                    message = "Member Information Invalid"
                });
            }
            string InvoiceNumber = executeReturnModel.merchantInvoiceNumber;
            MemberModel memberModel = _mapper.Map<MemberViewModel, MemberModel>(member);
            string ClubID = await InsertNewMember(memberModel);
            memberModel.ClubId = ClubID;
            //MemberInterest mInterest = member.Interests;
            mInterest.ClubId = ClubID;
            await InsertNewMemberInterest(mInterest);
            await InsertPicture(member.Picture, ClubID + memberModel.Picture);
            
            await _paymentService.InsertPaymentDB(executeReturnModel, ClubID, "General Member Recruitment",memberModel.Name,memberModel.Email,memberModel.MobileNo,"");


            SendSMSInitModel smsStart = PrepareConfirmationSMSGM(memberModel);
            SendSMSResultModel smsModel = await _smsService.SendSMS(smsStart);
            await _smsService.InsertSMSDB(smsStart, smsModel, ClubID, memberModel.Name, "General Member Recruitment");


            SendEmailInitModel sendConfirmationEmailGM = await PrepareConfirmationEmailGM(memberModel);
            await _emailService.SendEmailAsync(sendConfirmationEmailGM, "General Member Recruitment");

            SendEmailInitModel sendInvoiceEmailGM  = await PrepareInvoiceEmailGM(InvoiceNumber,memberModel.Email);
            await _emailService.SendEmailAsync(sendInvoiceEmailGM, "General Member Recruitment");

            return new JsonResult(new
            {
                status = true,
                message = "Success"
            });
        }*/
        private async Task<string> ConvertMemberFromTemp(string Token, string PictureExt)
        {
            try
            {
                var result = await _dapperDBContext.GetInfoAsync<string>(new
                {
                    Token = Token,
                    PictureExt = PictureExt

                }, ConvertMemberFromTempSP);
                return result;
            }
            catch (Exception ex)
            {
                return "";
            }
        }
        public async Task<JsonResult> InsertNewMemberFromTemp(BkashExecutePaymentReturnModel executeReturnModel,string Token) 
        {
            string InvoiceNumber = executeReturnModel.merchantInvoiceNumber;
            List<MemberModelTemp> tempList = await GetMemberListTemp();
            MemberModelTemp tempModel = tempList.Where(e => e.Token == Token).First();
            string ClubID = await ConvertMemberFromTemp(Token,tempModel.Picture.Substring(tempModel.Picture.IndexOf('.')));
            await RenamePicture(tempModel.Picture, ClubID);
            
            List<MemberModel> list = await GetMemberList();
            MemberModel memberModel = list.Where(e=>e.ClubId==ClubID).First();
            await _paymentService.InsertPaymentDB(executeReturnModel, ClubID, "General Member Recruitment", memberModel.Name, memberModel.Email, memberModel.MobileNo, "");


            SendSMSInitModel smsStart = PrepareConfirmationSMSGM(memberModel);
            SendSMSResultModel smsModel = await _smsService.SendSMS(smsStart);
            await _smsService.InsertSMSDB(smsStart, smsModel, ClubID, memberModel.Name, "General Member Recruitment");


            SendEmailInitModel sendConfirmationEmailGM = await PrepareConfirmationEmailGM(memberModel);
            SendEmailInitModel sendInvoiceEmailGM = await PrepareInvoiceEmailGM(InvoiceNumber, memberModel.Email);
            _emailService.SendEmailAsync(sendConfirmationEmailGM, "General Member Recruitment");
            _emailService.SendEmailAsync(sendInvoiceEmailGM, "General Member Recruitment");

            return new JsonResult(new
            {
                status = true,
                message = "Success"
            });
        }
        public async Task<JsonResult> InsertNewMemberTemp(MemberViewModel member,string Token)
        {
            MemberModelTemp memberModel = _mapper.Map<MemberViewModel, MemberModelTemp>(member);
            memberModel.Token = Token;
            MemberInterestTemp mInterest = member.Interests;
            mInterest.Token = Token;
            await InsertNewMemberTemp(memberModel);
            await InsertNewMemberInterestTemp(mInterest);
            await InsertPicture(member.Picture, Token + memberModel.Picture);

            return new JsonResult(new
            {
                status = true,
                message = "Success"
            });
        }
        private async Task<bool> RenamePicture(string OldName,string ClubId)
        {
            string extension = OldName.Substring(OldName.IndexOf('.'));
            string NewName = _webHostEnvironment.WebRootPath + "//img//members//"+ ClubId + extension;
            OldName = _webHostEnvironment.WebRootPath + "//img//members//temp//" + OldName;
            try
            {
                System.IO.File.Move(OldName,NewName);
            }
            catch(Exception e)
            {
                return false;
            }
            return true;
        }
        public async Task<string> GenerateTokenForMember()
        {
            try
            {
                var result = await _dapperDBContext.GetInfoAsync<string>(new
                {

                }, GenerateTokenForMemberSP);
                return result;
            }
            catch (Exception ex)
            {
                return "";
            }
        }
        public async Task<bool> VerifyTokenForMember(string Token)
        {
            try
            {
                var result = await _dapperDBContext.GetInfoAsync<int>(new
                {
                    Token = Token
                }, VerifyTokenForMemberSP);
                return result>0;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public async Task<List<MemberToken>> GetTokenForMember(string Token)
        {
            try
            {
                var result = await _dapperDBContext.GetInfoListAsync<MemberToken>(new
                {
                    Token = Token
                }, GetTokenForMemberSP);
                List<MemberToken> members = (List<MemberToken>)result;
                return members;
            }
            catch (Exception ex)
            {
                return new List<MemberToken>();
            }
        }
        public async Task<JsonResult> CheckMemberInformationNew(MemberViewModel member)
        {
            if (member == null || !NameValidation(member.Name) || !EmailValidation(member.Email) || !MobileNoValidation(member.MobileNo) ||
                 !StudentIdValidation(member.StudentId) || member.Department == null || member.Semester == null || member.BirthDate == null
                 || member.BloodGroup == null || !PictureValidation(member.Picture))
            {
                return new JsonResult(new
                {
                    status = false,
                    message = "Information Not Valid"
                });

            }
            long imageLen = member.Picture.Length;
            if(imageLen>1024*1024*10)
            {
                return new JsonResult(new
                {
                    status = false,
                    message = "Maximum Image Size is 10 MB. But your Image Size is " + ((int)(imageLen / (1024 * 1024))).ToString()+ " MB."
                });
            }
            List<MemberModel> memberModels = await GetMemberList();
            if (memberModels.Where(m => m.Email == member.Email).Any())
            {
                return new JsonResult(new
                {
                    status = false,
                    message = "Email Already Exist"
                });
            }
            else if (memberModels.Where(m => m.MobileNo == member.MobileNo).Any())
            {
                return new JsonResult(new
                {
                    status = false,
                    message = "Mobile Number Already Exist"
                });
            }
            else if (memberModels.Where(m => m.StudentId == member.StudentId).Any())
            {
                return new JsonResult(new
                {
                    status = false,
                    message = "Student ID Already Exist"
                });
            }
            return new JsonResult(new
            {
                status = true,
                message = "Success"
            });
        }

        private bool NameValidation(string name)
        {
            if (name == null) return false;
            var namePattern = new Regex(@"^([a-zA-Z]+\s)*[a-zA-Z]+$");
            return namePattern.IsMatch(name);
        }
        private bool EmailValidation(string email)
        {
            if (email == null) return false;
            var emailParts = email.Split('@');
            if (emailParts.Length != 2) return false;
            var account = emailParts[0];
            var address = emailParts[1];
            if (account == null || address == null || account.Length > 64 || address.Length > 255) return false;
            var domainParts = address.Split('.');
            if (domainParts == null || domainParts.Where(m => m.Length > 63).Any()) return false;
            var emailPattern = new Regex(@"^[-!#$%&'*+\/0-9=?A-Z^_a-z`{|}~](\.?[-!#$%&'*+\/0-9=?A-Z^_a-z`{|}~])*@[a-zA-Z0-9](-*\.?[a-zA-Z0-9])*\.[a-zA-Z](-?[a-zA-Z0-9])+$");
            return emailPattern.IsMatch(email);
        }
        private bool MobileNoValidation(string number)
        {
            if (number == null) return false;
            var pattern = new Regex(@"^01[3-9]\d{8}$");
            return pattern.IsMatch(number);
        }
        private bool StudentIdValidation(string Id)
        {
            if (Id == null) return false;
            var pattern = new Regex(@"^(\d{9}|\d{11})$");
            return pattern.IsMatch(Id);
        }
        private bool PictureValidation(IFormFile picture)
        {
            if (picture == null) return false;
            List<string> PermittedFileTypes = new List<string> { "image/jpeg", "image/png" };
            return PermittedFileTypes.Contains(picture.ContentType);
        }
        public async Task<bool> InsertPicture(IFormFile file, string fileName)
        {
            try
            {
                if (file.Length > 0)
                {
                    Directory.CreateDirectory("wwwroot/img/members/temp");
                    var filePath = Path.Combine(_webHostEnvironment.WebRootPath+"//img//members//temp//", fileName);

                    using (var stream = System.IO.File.Create(filePath))
                    {
                        await file.CopyToAsync(stream);
                    }
                }
            }
            catch(Exception e)
            {
                return false;
            }
            
            return true;
        }



        #endregion
    }
}
