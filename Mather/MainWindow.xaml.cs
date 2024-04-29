using Mather.Authorization;
using System.Windows;

namespace Mather
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Profile profile = new Profile("t", "t", Profile.Type.Teacher);
            profile.SaveProfile();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string login = LoginBox.Text;
            string password = PasswordBox.Text;
            Profile? profile = Profile.CheckProfile(login, password);
            if (profile == null)
            {
                return;
            }
            if (profile.GetProfileType() == Profile.Type.Student)
            {
                StudentWindow studentWindow = new StudentWindow();
                studentWindow.Show();
            }
            else if (profile.GetProfileType() == Profile.Type.Teacher)
            {
                TeacherWindow teacherWindow = new TeacherWindow();
                teacherWindow.Show();
            }
            this.Close();
        }
    }
}