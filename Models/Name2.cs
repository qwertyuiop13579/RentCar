using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Labs.Models
{
    public class Name2
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
    }
}