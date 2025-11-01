using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gradeStudent.Model
{
    public class Course
    {
        [Key]
        public int Cid { get; set; }
        [Required]
        public string CName { get; set; }
        public int CTeacherid {  get; set; }
        [ForeignKey("CTeacherid")]
        public User user { get; set; }
    }
}
