using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using gradeStudent.Model;
using Microsoft.EntityFrameworkCore;

namespace gradeStudent.Data
{
    public class GradContext:DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }
        public DbSet<Grade> Grades { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=DESKTOP-0OSQ4LI;Initial Catalog=GradeBookSystem;Integrated Security=True;Encrypt=True;Trust Server Certificate=True");
        }
    }
}
