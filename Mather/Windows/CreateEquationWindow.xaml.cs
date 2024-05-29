using Mather.Data.Tasks.Equations;
using Mather.Data.Tasks.Equations.Operations;
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
    /// Логика взаимодействия для CreateEquationWindow.xaml
    /// </summary>
    public partial class CreateEquationWindow : Window
    {
        public EquationElement Target { get; set; }
        public EquationElement Element { get; set; }
        private int _index;
        public CreateEquationWindow(EquationElement target)
        {
            InitializeComponent();
            Target = target;
        }

        private void OperationTypeCombobox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _index = ((ComboBox)sender).SelectedIndex;
            if (_index != null)
            {
                OperationElementTypeCombobox.IsEnabled = true;
                TypeTextbox.IsEnabled = true;
            }
            if (_index == 6)
            {
                OperationElementTypeCombobox.SelectedItem = null;
                TypeTextbox.Text = string.Empty;
                OperationElementTypeCombobox.IsEnabled = false;
                TypeTextbox.IsEnabled = false;
                OkButton.IsEnabled = true;
            }
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                bool isValid = CheckTextbox(OperationElementTypeCombobox.SelectedIndex);
                if (isValid)
                {
                    if (_index == 6)
                    {
                        Element = new Brackets(Target);
                        DialogResult = true;
                        return;
                    }
                    var element = CreateElement(OperationElementTypeCombobox.SelectedIndex);
                    if (_index == 0)
                    {
                        Element = new Addition(Target, element);
                    }
                    else if (_index == 1)
                    {
                        Element = new Subtraction(Target, element);
                    }
                    else if (_index == 2)
                    {
                        Element = new Multiplication(Target, element);
                    }
                    else if (_index == 3)
                    {
                        Element = new Division(Target, element);
                    }
                    else if (_index == 4)
                    {
                        Element = new Pow(Target, element);
                    }
                    else if (_index == 5)
                    {
                        Element = new Root(Target, element);
                    }
                    DialogResult = true;
                }
            }
            catch
            {
                MessageBox.Show("Введено неверное значение!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                TypeTextbox.Text = "";
            }
        }

        bool CheckTextbox(int typeIndex)
        {
            if (_index == 6) return true;
            if (typeIndex == 0)
            {
                return TypeTextbox.Text.All(c => char.IsDigit(c));
            }
            else if (typeIndex == 1)
            {
                TypeTextbox.Text = TypeTextbox.Text.ToLower();
                TypeTextbox.Text = TypeTextbox.Text.Trim();
                return TypeTextbox.Text.Length == 1 || char.IsSymbol(TypeTextbox.Text[0]);
            }
            else if (typeIndex == 2)
                return true;
            return false;
        }

        EquationElement CreateElement(int typeIndex)
        {
            if (typeIndex == 0)
            {
                return new Constant(int.Parse(TypeTextbox.Text));
            }
            else if (typeIndex == 1)
            {
                return new Variable(TypeTextbox.Text);
            }
            throw new Exception();
        }

        private void OperationElementTypeCombobox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            OkButton.IsEnabled = true;
        }
    }
}
