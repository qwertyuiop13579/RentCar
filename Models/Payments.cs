using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Labs.Models
{
    public class Payments
    {
        [Key]
        public int Id { get; set; }
        public DateTime date { get; set; }
        public int price { get; set; }
        public string account_number { get; set; }
        public string payer_number { get; set; }
    }
}
