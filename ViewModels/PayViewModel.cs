using Labs.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Labs.ViewModels
{
    public class PayViewModel
    {
        public int SaleId { get; set; }
        public decimal Sum { get; set; }
        public string Model { get; set; }
        public string Mark { get; set; }
        public DateTime date2 { get; set; }
        public DateTime date3 { get; set; }
    }
}
