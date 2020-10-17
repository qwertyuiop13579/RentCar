using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Labs.Models
{
    public class Client
    {
        [Key]
        public int Id { get; set; }
        public int id_passport { get; set; }
        public int id_address { get; set; }
    }
}
