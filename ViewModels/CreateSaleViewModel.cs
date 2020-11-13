using Labs.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Labs.ViewModels
{
    public class CreateSaleViewModel
    {
        [Required]
        public int id_client { get; set; }
        public int id_car { get; set; }
        public double rentprice { get; set; }
        [Required]
        public DateTime date2 { get; set; }
        [Required]
        public DateTime date3 { get; set; }
        public int summ { get; set; }
    }
}
