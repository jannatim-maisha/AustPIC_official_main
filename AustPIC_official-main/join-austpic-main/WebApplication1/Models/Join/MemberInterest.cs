using System.ComponentModel.DataAnnotations;
namespace ClubWebsite.Models.Join
{
    public class MemberInterest
    {
        public string? ClubId { get; set; }
        public bool IsCompetitiveProgramming { get; set; }
        public bool IsSoftwareDevelopment { get; set; }
        public bool IsWebDevelopment { get; set; }
        public bool IsMobileAppDevelopment { get; set; }
        public bool IsEventManagement { get; set; }
        public bool IsGraphicsDesign { get; set; }
        public bool IsPhotography { get; set; }
        public bool IsRobotics { get; set; }
        public bool IsArtificialIntelligence { get; set; }
    }
}
