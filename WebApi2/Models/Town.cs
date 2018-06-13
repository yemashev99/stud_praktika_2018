using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApi2.Models
{
    public class Town
    {
        public int Id { get; set; }
        public string name_town { get; set; }
        public virtual ICollection<Student> Students { get; set; }
    }
}