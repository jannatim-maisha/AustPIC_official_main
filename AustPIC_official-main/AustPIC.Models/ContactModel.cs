using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace AustPIC.Models
{
    public class ContactModel
    {
        //public int ContactId { get; set; }

        [Required]
        [MaxLength(100)]
        [DisplayName("Name")]
        public string ContactName { get; set; }

        [Required]
        [MaxLength(50)]
        [DisplayName("Email")]
        public string ContactEmail { get; set; }

        [Required]
        [MaxLength(1000)]
        [DisplayName("Message")]
        public string ContactMessage { get; set; }

        //public System.DateTime ContactTime { get; set; }
        //public int ContactReplied { get; set; }

        [Required]
        [MaxLength(50)]
        [DisplayName("Subject")]
        public string ContactSubject { get; set; }
    }

}
