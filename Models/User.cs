using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Labs.Models
{
    public class User
    {
        [Key]
        
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]    
        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int RoleId { get; set; }

        public int StatusId { get; set; }        
        public DateTime dataofregistration { get; set; }
        public DateTime? dataofbeginblock { get; set; }
        public DateTime? dataofendbock { get; set; }

        public int IdName1 { get; set; }
        public int IdName2 { get; set; }
        public int IdName3 { get; set; }
        public int id_client { get; set; }
    }
}
