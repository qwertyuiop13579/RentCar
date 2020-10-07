using Labs.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Labs.ViewModels
{
    public class ChangeRoleViewModel
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string RoleName { get; set; }
    }
}
