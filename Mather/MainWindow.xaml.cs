using Mather.Authorization;
using Mather.Windows;
using System.IO;
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
            string savePath = Environment.ExpandEnvironmentVariables(@"%appdata%\Mather");
            if (!Directory.Exists(savePath))
            {
                Directory.CreateDirectory(savePath);
            }
            var files = Directory.GetFiles(savePath);
            if (files.Length == 0)
            {
                MessageBox.Show("Ни одной чётной записи не существует.\n\nДля продолжения необходимо создать профиль учителя.", "Профилей не существует", MessageBoxButton.OK, MessageBoxImage.Warning);
                RegistrationWindow window = new RegistrationWindow();
                window.ShowDialog();
                if(window.DialogResult == false)
                {
                    this.Close();
                }
            }

            InitializeComponent();
            //Profile profile = new Profile("t", "t", Profile.Type.Teacher);
            //profile.SaveProfile();
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