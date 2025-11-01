using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gradeStudent.Model
{
    public class Enrollment
    {
        [Key]
       public int EnrollmentID {  get; set; }
        public DateTime EDate {  get; set; }
        public int CourseID {  get; set; }
        public int usid { get; set; }

        [ForeignKey("CourseID")]
        public Course course { get; set; }
        [ForeignKey("usid")]
        public User user { get; set; }
        public ICollection<Grade> grades { get; set; }
    }
}
