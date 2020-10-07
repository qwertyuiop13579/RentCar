using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Labs.Models
{
    public class Passport
    {
        [Key]
        public int Id { get; set; }
        public string passport1 { get; set; }
        public int passport2 { get; set; }
        public string passport3 { get; set; }
        public DateTime date1 { get; set; }
        public DateTime date2 { get; set; }
        public string authority { get; set; }
        public Sex sex { get; set; }
        public DateTime date3 { get; set; }
        public int id_name1 { get; set; }
        public int id_name2 { get; set; }
        public int id_name3 { get; set; }
    }
    public enum Sex
    {
        м,
        ж
    }
}