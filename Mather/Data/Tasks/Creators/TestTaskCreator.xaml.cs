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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Mather.Data.Tasks.Creators
{
    /// <summary>
    /// Логика взаимодействия для TestTaskCreator.xaml
    /// </summary>
    public partial class TestTaskCreator : UserControl
    {
        private TestTask testTask;
        public TestTaskCreator()
        {
            InitializeComponent();

        }
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            if (this.DataContext is TestTask testTask)
            {
                this.testTask = testTask;
            }
        }
        private void AddTaskElementButton_Click(object sender, RoutedEventArgs e)
        {
            testTask.Checks.Add(new TestTaskElement());
        }

        private void Remove_Click(object sender, RoutedEventArgs e)
        {
            var textbox = (((StackPanel)((Button)sender).Parent).Children[1]) as TextBox;
            var context = textbox.DataContext as TestTaskElement;
            testTask.Checks.Remove(context);
        }

        private void UserControl_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (this.DataContext is TestTask testTask)
            {
                this.testTask = testTask;
            }
        }
    }
}
