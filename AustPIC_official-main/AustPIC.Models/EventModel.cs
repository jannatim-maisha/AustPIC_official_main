using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace AustPIC.Models
{
    public class EventModel
    {
        public int EventId { get; set; }
        public string EventTitle { get; set; }
        public string EventDesc { get; set; }
        public string EventImg { get; set; }

        //[DataType(DataType.Date)]
        //[DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public System.DateTime EventStart { get; set; }

        //[DataType(DataType.Date)]
        //[DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public System.DateTime EventEnd { get; set; }
    }

}
