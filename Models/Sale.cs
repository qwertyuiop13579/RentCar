using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Labs.Models
{
    public class Sale
    {
        [Key]
        public int Id { get; set; }
        public DateTime date1 { get; set; }
        public int id_client { get; set; }
        public int id_car { get; set; }
        public int id_payment { get; set; }
        public DateTime date2 { get; set; }
        public DateTime date3 { get; set; }
        public int price { get; set; }
        public string status { get; set; }

    }
}
