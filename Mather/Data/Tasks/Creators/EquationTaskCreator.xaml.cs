using Mather.Data.Tasks.Equations;
using System.Text.RegularExpressions;
using System.Windows.Controls;

namespace Mather.Data.Tasks.Creators
{
    /// <summary>
    /// Логика взаимодействия для EquationTaskCreator.xaml
    /// </summary>
    public partial class EquationTaskCreator : UserControl
    {
        private EquationTask task;
        public Equation Equation { get; set; }
        public EquationTaskCreator()
        {
            InitializeComponent();
        }
        private void UserControl_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            task = DataContext as EquationTask;
            Equation = task.Equation;
        }

        private void AnswerTextbox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (double.TryParse(AnswerTextbox.Text, out double result))
                task.Answer = result;
        }

        private static readonly Regex _regex = new Regex("[^0-9,.-]+"); //regex that matches disallowed text
        private static bool IsTextAllowed(string text)
        {
            return !_regex.IsMatch(text);
        }

        private void AnswerTextbox_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            e.Handled = !IsTextAllowed(e.Text);
        }
    }
}
