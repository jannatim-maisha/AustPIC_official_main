using ClubWebsite.Models.Join;
using System.ComponentModel.DataAnnotations;

namespace ClubWebsite.Models.Common
{
    public class MemberModel
    {
        public string? ClubId { get; set; }
        public string? Name { get; set; }
        public string? StudentId { get; set; }
        public string? Email { get; set; }
        public string? Department { get; set; }
        public string? Semester { get; set; }
        public string? MobileNo { get; set; }
        [DataType(DataType.Date)]
        public DateTime? BirthDate { get; set; }
        public string? BloodGroup { get; set; }
        public string? Picture { get; set; }
        public DateTime? JoinTime { get; set; }
    }
}
