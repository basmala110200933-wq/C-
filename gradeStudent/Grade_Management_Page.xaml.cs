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
using gradeStudent.Model;

namespace gradeStudent
{
    /// <summary>
    /// Interaction logic for Grade_Management_Page.xaml
    /// </summary>
    public partial class Grade_Management_Page : Window
    {
        GradContext con = new GradContext();
        public Grade_Management_Page()
        {
            InitializeComponent();
            LoadData();
        }

        public void LoadData()
        {
            using (var context = new GradContext())
            {
                var Courses = context.Courses.ToList();
                CourseBox.ItemsSource = Courses;
                CourseBox.DisplayMemberPath = "CName";

                var Student = context.Users.Where(u => u.URole == "Student").ToList();
                studentBox.ItemsSource = Student;
                studentBox.DisplayMemberPath = "UName";

                var grade = context.Grades.ToList();
                dgGrade.ItemsSource = grade;
            }
        }

        private void Logout_click(object sender, RoutedEventArgs e)
        {
            MainWindow m = new MainWindow();
            m.Show();
            this.Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (studentBox.SelectedItem == null || CourseBox.SelectedItem == null)
            {
                MessageBox.Show("Please select both Student and Course!");
                return;
            }

            if (string.IsNullOrWhiteSpace(txtxAssign.Text))
            {
                MessageBox.Show("Please enter Assignment!");
                return;
            }
            if (!decimal.TryParse(txtScore.Text, out decimal score))
            {
                MessageBox.Show("Please enter valid score!");
                return;
            }
            if (!decimal.TryParse(txtMaxScore.Text, out decimal Maxscore))
            {
                MessageBox.Show("Please enter valid MaxScore!");
                return;
            }

            var selectedStudent = (User)studentBox.SelectedItem;
            var selectedCourse = (Course)CourseBox.SelectedItem;

            var enrollment = con.Enrollments
                .FirstOrDefault(e => e.usid == selectedStudent.Userid && e.CourseID == selectedCourse.Cid);

            if (enrollment == null)
            {
                MessageBox.Show("This student is not enrolled in the selected course!");
                return;
            }

            var newGrade = new Grade()
            {
                AssignmentName = txtxAssign.Text,
                Score = score,
                MaxScore = Maxscore,
                EnrollId = enrollment.EnrollmentID
            };

            con.Grades.Add(newGrade);
            con.SaveChanges();

            var studentGrades = con.Grades
                .Where(g => g.EnrollId == enrollment.EnrollmentID)
                .ToList();

            if (studentGrades.Count > 0)
            {
                double avg = studentGrades.Average(g => (double)g.Score);
                MessageBox.Show($"Grade added successfully!\nAverage Score: {avg:F2}");
            }
            else
            {
                MessageBox.Show("Grade added successfully!");
            }

            txtMaxScore.Clear();
            txtScore.Clear();
            txtxAssign.Clear();
            LoadData();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
                if (dgGrade.SelectedItem == null)
                {
                    MessageBox.Show("Please select a grade from the DataGrid first.");
                    return;
                }

                var selectedGrade = (Grade)dgGrade.SelectedItem;

                if (string.IsNullOrWhiteSpace(txtxAssign.Text) &&
                    string.IsNullOrWhiteSpace(txtScore.Text) &&
                    string.IsNullOrWhiteSpace(txtMaxScore.Text))
                {
                    txtxAssign.Text = selectedGrade.AssignmentName;
                    txtScore.Text = selectedGrade.Score.ToString();
                    txtMaxScore.Text = selectedGrade.MaxScore.ToString();
                    MessageBox.Show("Now you can edit the fields and click Edit again to save.");
                }
                else
                {
                    if (decimal.TryParse(txtScore.Text, out decimal scoreVal) &&
                        decimal.TryParse(txtMaxScore.Text, out decimal maxVal))
                    {
                        selectedGrade.AssignmentName = txtxAssign.Text;
                        selectedGrade.Score = scoreVal;
                        selectedGrade.MaxScore = maxVal;

                        con.Grades.Update(selectedGrade);
                        con.SaveChanges();

                        var studentGrades = con.Grades
                            .Where(g => g.EnrollId == selectedGrade.EnrollId)
                            .ToList();

                        double avg = studentGrades.Average(g => (double)g.Score);

                        dgGrade.Items.Refresh();

                        MessageBox.Show($"Grade updated successfully!\nNew Average Score: {avg:F2}");

                        txtxAssign.Clear();
                        txtScore.Clear();
                        txtMaxScore.Clear();
                    }
                    else
                    {
                        MessageBox.Show("Please enter valid numbers for Score and MaxScore.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            try
            {
                if (dgGrade.SelectedItem == null)
                {
                    MessageBox.Show("Please select to delete");
                    return;
                }
                var result = MessageBox.Show("Are you sure ?", "Confirm!!", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    var select = (Grade)dgGrade.SelectedItem;
                    con.Grades.Remove(select);
                    con.SaveChanges();
                    MessageBox.Show("Done Delete");
                    LoadData();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error : " + ex.Message);
            }
        }

        private void dgGrade_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (dgGrade.SelectedItem != null)
                {
                    var select = dgGrade.SelectedItem as Grade;
                    if (select != null)
                    {
                        txtxAssign.Text = select.AssignmentName;
                        txtScore.Text = select.Score.ToString();
                        txtMaxScore.Text = select.MaxScore.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error : " + ex.Message);
            }
        }
    }
}
