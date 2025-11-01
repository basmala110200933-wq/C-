using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using gradeStudent.Data;
using Microsoft.EntityFrameworkCore;

namespace gradeStudent
{
    public partial class View_Grades_Page : Window
    {
        public View_Grades_Page()
        {
            InitializeComponent();
            LoadGrades();
            CalculateAVG();
        }

        public void LoadGrades()
        {
            try
            {
                using (var con = new GradContext())
                {
                    var gg = con.Grades
                        .Include(g => g.enrollment)
                        .ThenInclude(e => e.user)
                        .Include(g => g.enrollment)
                        .ThenInclude(e => e.course)
                        .ToList(); 

                    dgGrade.ItemsSource = gg;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error : " + ex.Message);
            }
        }

        public void CalculateAVG()
        {
            try
            {
                using (var con = new GradContext())
                {
                    var grade = con.Grades.ToList();
                    if (grade.Any())
                    {
                        int count = 0;
                        decimal total = 0;
                        foreach (var g in grade)
                        {
                            if (g.MaxScore > 0)
                            {
                                decimal precent = (g.Score / g.MaxScore) * 100;
                                total += precent;
                                count++;
                            }
                        }

                        if (count > 0)
                        {
                            var avg = total / count;
                            AVG.Content = $"AVG : {avg:F2}%";
                        }
                        else
                        {
                            AVG.Content = "Invalid calculate";
                        }
                    }
                    else
                    {
                        AVG.Content = "No grades";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error : " + ex.Message);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow m = new MainWindow();
            m.Show();
            this.Close();
        }
    }
}
