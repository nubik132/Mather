using System.Windows;

namespace Mather.Windows
{
    /// <summary>
    /// Логика взаимодействия для AddTaskWindow.xaml
    /// </summary>
    public partial class AddTaskWindow : Window
    {
        public enum Tasks
        {
            Test, Plot, Equation
        }
        public Tasks SelectedTask { get; private set; }
        public AddTaskWindow()
        {
            InitializeComponent();
            TaskListBox.ItemsSource = new List<string>() { "Тест", "График", "Уравнение" };
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            switch (TaskListBox.SelectedValue.ToString())
            {
                case "Тест":
                    SelectedTask = Tasks.Test; break;
                case "График":
                    SelectedTask = Tasks.Plot; break;
                case "Уравнение":
                    SelectedTask = Tasks.Equation; break;
            }
            this.DialogResult = true;

        }
    }
}
