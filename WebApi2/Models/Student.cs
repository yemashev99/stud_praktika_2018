using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApi2.Models
{
    public class Student
    {
        public int Id { get; set; }
        public string FIO { get; set; }
        public int TownId { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-mm-dd}", ApplyFormatInEditMode = true)]
        public DateTime birth { get; set; }
        public int kurs { get; set; }
        public bool dorm { get; set; }
        public virtual Town Town { get; set; }
    }
}