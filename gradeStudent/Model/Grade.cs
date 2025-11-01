using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gradeStudent.Model
{
    public class Grade
    {
        [Key]
        public int GradeID {  get; set; }
        public int EnrollId { get; set; }
        [ForeignKey("EnrollId")]
        public Enrollment enrollment {  get; set; }
        [Required]
        public string AssignmentName { get; set; }
      public decimal Score {  get; set; }
        public decimal MaxScore {  get; set; }

    }
}
