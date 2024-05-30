using Mather.Authorization;
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

namespace Mather.Windows
{
    /// <summary>
    /// Логика взаимодействия для RegistrationWindow.xaml
    /// </summary>
    public partial class RegistrationWindow : Window
    {
        public Profile Profile { get; set; }
        public RegistrationWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (LoginBox.Text != string.Empty && PasswordBox.Text != string.Empty && ((bool)StudentRadio.IsChecked || (bool)TeacherRadio.IsChecked))
            {
                Profile = new Profile(LoginBox.Text, PasswordBox.Text, (bool)StudentRadio.IsChecked ? Profile.Type.Student : Profile.Type.Teacher);
                Profile.SaveProfile();
                DialogResult = true;
            }
        }
    }
}
