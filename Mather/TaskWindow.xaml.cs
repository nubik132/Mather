using Mather.Data.States;
using Mather.Data.Tasks;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace Mather
{
    /// <summary>
    /// Логика взаимодействия для TaskWindow.xaml
    /// </summary>
    public partial class TaskWindow : Window
    {
        public Data.Tasks.Task Task { get; set; }
        public TaskWindow(Data.Tasks.Task task) : base()
        {
            Task = task;
        }
        private void TreeViewItem_Selected(object sender, RoutedEventArgs e)
        {
            if (sender is TreeView tree && tree.SelectedItem is State state)
            {
                DocumentViewer.Document = state.Document;
            }
            e.Handled = true;
        }
        public void LoadStates(ObservableCollection<StateBranch> collection)
        {
            StatesTreeView.ItemsSource = collection;
        }

        public void LoadTask(ObservableCollection<StateBranch> collection)
        {
            StatesTreeView.ItemsSource = collection;
        }
    }
}
