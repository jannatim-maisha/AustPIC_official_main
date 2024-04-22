using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace AustPIC.Models
{
    public class CommitteeModel
    {
        public int CommitteeId { get; set; }

        [Required]
        [MaxLength(50)]
        [DisplayName("Name")]
        public string CommitteeName { get; set; }

        [Required]
        [MaxLength(50)]
        [DisplayName("Position")]
        public string CommitteePosition { get; set; }

        [Required]
        [MaxLength(50)]
        [DisplayName("Image")]
        public string CommitteeImg { get; set; }

        [Required]
        [MaxLength(50)]
        [DisplayName("Details")]
        public string CommitteeDetails { get; set; }

        [Required]
        [DisplayName("Year")]
        public int CommitteeYear { get; set; }

        [Required]
        [DisplayName("Semester")]
        public int CommitteeSemester { get; set; }

        [Required]
        [MaxLength(50)]
        [DisplayName("Facebook")]
        public string CommitteeFacebook { get; set; }

        [Required]
        [EmailAddress]
        [DisplayName("Email")]
        public string CommitteeEmail { get; set; }
        [Required]
        [DisplayName("Rank Order")]
        public int CommitteeStatus { get; set; }

        public string CommitteeIteration { get; set; }
    }
}
