using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Labs.ViewModels
{
    public class ChangeStatusViewModel
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string StatusName { get; set; }
        public DateTime Dateofendblock { get; set; }

        public bool IsForever { get; set; }

    }
}
