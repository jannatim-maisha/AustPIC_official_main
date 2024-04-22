using ClubWebsite.Models.Common;
using ClubWebsite.Models;
using ClubWebsite.Service.Join;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Diagnostics.Metrics;
using System.Net;
using Microsoft.Extensions.FileSystemGlobbing.Internal;
using System;
using AutoMapper;
using ClubWebsite.Mapper;
using ClubWebsite.Infrastructure;
using RestSharp;
using AutoMapper.Execution;
using ClubWebsite.Models.Join;
using ClubWebsite.Service.Sms;
using ClubWebsite.Models.Sms;
using Microsoft.Extensions.Configuration;
using ClubWebsite.Service.Payment;
using ClubWebsite.Models.Payment;
using ClubWebsite.Service.Email;
using ClubWebsite.Models.Email;
using ClubWebsite.Service.Template;
using Org.BouncyCastle.Asn1.X509;
using static System.Net.Mime.MediaTypeNames;
using System.Drawing;
using System.Reflection;
using System.Text.Json.Serialization;
using System.Web;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace ClubWebsite.Controllers
{
    public class JoinController : Controller
    {
        private readonly IJoinService _joinService;
        public JoinController(IJoinService joinService)
        {
            _joinService = joinService;
        }
        public async Task<IActionResult> Index()
        {
            ViewBag.Departments = await _joinService.GetDepartmentList();
            ViewBag.Semesters = await _joinService.GetSemesterList();
            ViewBag.IsExistingReg = false;
            ViewBag.Fee = "105";
            try
            {
                var cookie = HttpContext.User.FindFirstValue("Join-AUSTPIC-Member-Token");
                if (cookie != null && await _joinService.VerifyTokenForMember(cookie))
                {
                    ViewBag.IsExistingReg = true;
                } 
            }
            catch(Exception e)
            {
            }
            return View();
        }
        public IActionResult Success()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> CheckMemberInformationNew(MemberViewModel member)
        {
            var req = HttpContext.Request;
            JsonResult json =  await _joinService.CheckMemberInformationNew(member);
            if (json.Value.ToString().Contains("status = True"))
            {
                /*CookieOptions option = new CookieOptions();
                option.Path = "/";
                option.Expires = DateTime.Now.AddDays(1);
                option.Secure = true;
                option.HttpOnly = true;*/
                string Token = await _joinService.GenerateTokenForMember();
                //HttpContext.Response.Cookies.Append("Join-AUSTPIC-Web-App-Member", Token, option);
                var claims = new List<Claim>
                {
                    new Claim("Join-AUSTPIC-Member-Token", Token)
                };

                var claimsIdentity = new ClaimsIdentity(
                    claims, CookieAuthenticationDefaults.AuthenticationScheme);

                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity));
                await _joinService.InsertNewMemberTemp(member, Token);
            }
            return json;
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
