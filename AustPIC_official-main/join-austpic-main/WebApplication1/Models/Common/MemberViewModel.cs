using ClubWebsite.Models.Join;
using System.ComponentModel.DataAnnotations;
namespace ClubWebsite.Models.Common
{
    public class MemberViewModel
    {
        public string? ClubId { get; set; }
        public string? Name { get; set; }
        public string? StudentId { get; set; }
        public string? Email { get; set; }
        public string? Department { get; set; }
        public string? Semester { get; set; }
        public string? MobileNo { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? BirthDate { get; set; }
        public string? BloodGroup { get; set; }
        public IFormFile? Picture { get; set; }
        public DateTime? JoinTime { get; set; }
        public MemberInterestTemp Interests { get; set; }
    }
}
