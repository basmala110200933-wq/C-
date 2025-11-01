using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gradeStudent.Model
{
    public class User
    {
        [Key]
        public int Userid { get; set; }
        [Required]
        public string UName { get; set; }
        [Required]
        public string UPassword { get; set; }
        [Required]
        public string UEmail { get; set; }
        public string URole { get; set; }
        public ICollection<Course> courses { get; set; }
        public ICollection<Enrollment>  enrollments { get; set; }


    }
}
