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
    }
}
