using Mather.Data.States;
using Mather.Data.Tasks;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using Task = Mather.Data.Tasks.Task;

namespace Mather
{
    /// <summary>
    /// Логика взаимодействия для TaskWindow.xaml
    /// </summary>
    public partial class TaskWindow : Window
    {
        public ObservableCollection<Task> Tasks { get; set; }
        private bool IsEnd { get; set; }
        public TaskWindow(TaskState branch) : base()
        {
            InitializeComponent();
            IsEnd = false;
            Tasks = branch.Tasks;
            this.Title = branch.Header;
            LoadTask(Tasks);
        }
        private void TreeViewItem_Selected(object sender, RoutedEventArgs e)
        {
            if (sender is TreeView tree && tree.SelectedItem is Task task)
            {
                DocumentViewer.Document = task.Document;
                DisplayTask(task);
            }
            e.Handled = true;
        }
        private void DisplayTask(Task task)
        {
            if (task is TestTask testTask)
                TaskControl.ItemsSource = testTask.Checks;
        }
        public void LoadStates(ObservableCollection<StateBranch> collection)
        {
            StatesTreeView.ItemsSource = collection;
        }

        public void LoadTask(ObservableCollection<Task> collection)
        {
            StatesTreeView.ItemsSource = collection;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!IsEnd)
            {
                var result = MessageBox.Show("Вы дейстивительно хотите завершить выполнение задания?\n\nОтветы нельзя будет изменить.", "Выйти?", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.No)
                {
                    e.Cancel = true;
                    return;
                }
                ShowResult();
                DocumentViewer.Document = new FlowDocument();
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //MessageBox.Show(CalculateResult().ToString());
            IsEnd = true;
            ShowResult();
            this.Close();
        }
        private void ShowResult()
        {
            var result = CalculateResult();
            ResultWindow window = new ResultWindow(result);
            window.ShowDialog();
        }

        private double CalculateResult()
        {
            return Math.Round(Tasks.Sum(task => task.GetResult()) / Tasks.Count, 2);
        }
    }
}
