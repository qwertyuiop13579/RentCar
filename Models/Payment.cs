using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Labs.Models
{
    public class Payment
    {
        [Key]
        public int Id { get; set; }
        public DateTime date { get; set; }    //дата оплаты
        public decimal? amount { get; set; } // сумма, которую заплатали с учетом комиссии
        public decimal? withdrawAmount { get; set; } // сумма, которую заплатали без учета комиссии
        public string sender { get; set; } // отправитель - кошелек в ЯД
        public string operation_Id { get; set; } // id операции в ЯД

    }
}
