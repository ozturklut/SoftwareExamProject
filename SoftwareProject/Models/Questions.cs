using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SoftwareProject.Models
{
    [Table("questions")]
    public class Questions
    {
        [Key]
        public int id { get; set; }
        public string Question { get; set; }
        public string A { get; set; }
        public string B { get; set; }
        public string C { get; set; }
        public string D { get; set; }
        public string Answer { get; set; }
        public int ExamID { get; set; }
        public virtual Exam Exam { get; set; }

    }
}
