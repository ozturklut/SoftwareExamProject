using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SoftwareProject.Models
{
    [Table("exam")]
    public class Exam
    {
        [Key]
        public int id { get; set; }
        public string Header { get; set; }
        public string Content { get; set; }
        public DateTime Date { get; set; }
        public ICollection<Questions> Questions { get; set; }

    }
}
