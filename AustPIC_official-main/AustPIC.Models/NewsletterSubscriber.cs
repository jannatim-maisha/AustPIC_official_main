using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AustPIC.Models
{
    public class NewsletterSubscriber
    {
        public int id { get; set; }
        public string email { get; set; }
        public DateTime subscribed_at { get; set; }
    }
}
