using Labs.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Labs.ViewModels
{
    public class EditClientViewModel
    {
        public int Id { get; set; }
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
        public string surname { get; set; }

        [Required(ErrorMessage = "Не указано имя")]
        public string name { get; set; }
        public string patronymic { get; set; }



        public string country { get; set; }
        public TypeCity type1 { get; set; }
        public string city { get; set; }
        public TypeStreet type2 { get; set; }
        public string street { get; set; }
        public int numhouse { get; set; }
        public int numapartment { get; set; }

        public string index { get; set; }
        public string housephone { get; set; }
        public string mobilephone { get; set; }
        public string email { get; set; }
    }
}
