using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Labs.Models
{
    public class Supplier
    {
        [Key]
        public int Id { get; set; }
        public string firmname { get; set; }
        public int id_address { get; set; }
        public string unn { get; set; }
        public int id_client { get; set; }
    }
}
