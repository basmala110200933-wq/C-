using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using gradeStudent.Data;
using gradeStudent.Model;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace gradeStudent
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        GradContext con = new GradContext();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var username = txtUser.Text;
            var password = txtPass.Password;
            if (string.IsNullOrWhiteSpace(txtPass.Password) || string.IsNullOrWhiteSpace(txtUser.Text))
            {
                MessageBox.Show("Enter all data");
            }
                var user=con.Users.FirstOrDefault(u=>u.UName==username&&u.UPassword==password);
                if(user!=null)
                {
                mytextTrue.Text = "Login successfull!";
                    if (user.URole == "Student")
                    {
                    View_Grades_Page v = new View_Grades_Page();
                    v.Show();
                    this.Close();
                    }
                    else if (user.URole == "Teacher")
                    {
                    Grade_Management_Page g = new Grade_Management_Page();
                    g.Show();
                    this.Close();
                    }
            }
            else
            {
                mytextfalse.Text = "Invalid username or password";
            }
               
            
        }
    }
}