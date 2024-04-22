using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace AustPIC.Models
{
    public class ContestModel
    {
        public int ContestId { get; set; }
        public string ContestTitle { get; set; }
        public string ContestDesc { get; set; }
        public string ContestImg { get; set; }
        public System.DateTime ContestStart { get; set; }
        public System.DateTime ContestEnd { get; set; }
    }

}
