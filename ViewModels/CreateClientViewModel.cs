using Labs.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Labs.ViewModels
{
    public class CreateClientViewModel
    {
        [MaxLength(2)]
        public string passport1 { get; set; }     //КВ
        public int passport2 { get; set; }             //12124521
        public string passport3 { get; set; }         //ID
        public DateTime date1 { get; set; }      //дата выдачи паспорта
        public DateTime date2 { get; set; }        //дата окончания паспорта
        public string authority { get; set; }     //огран выдавший паспорт
        public Sex sex { get; set; }      //пол
        public DateTime date3 { get; set; }

        [Required(ErrorMessage = "Не указана фамилия")]
        public string Name1 { get; set; }

        [Required(ErrorMessage = "Не указано имя")]
        public string Name2 { get; set; }
        public string Name3 { get; set; }



        public string country { get; set; }
        public string region { get; set; }
        public string district { get; set; }
        public TypeCity type1 { get; set; }
        public string city { get; set; }
        public TypeStreet type2 { get; set; }


        public string street { get; set; }

        [MaxLength(4)]
        public string num1 { get; set; }
        public int num2 { get; set; }
        [MaxLength(5)]
        public string num3 { get; set; }

        public int index { get; set; }
        public int num4 { get; set; }
        public int code { get; set; }
        public int mobile { get; set; }
        public string email { get; set; }

        public string bank_name { get; set; }
    }
}
