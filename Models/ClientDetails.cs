using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Labs.Models
{
    public class ClientDetails
    {
        public string surname { get; set; }
        public string name { get; set; }
        public string patronymic { get; set; }

        public string country { get; set; }
        public string city { get; set; }
        public string mobilephone { get; set; }
        public string email { get; set; }
    }
}
