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
        public DateTime dateofregistration { get; set; }
        public int id_role { get; set; }
        public int id_status { get; set; }                
        public DateTime dateofbeginblock { get; set; }
        public DateTime dateofendbock { get; set; }
        public int? id_client { get; set; }
    }
}
