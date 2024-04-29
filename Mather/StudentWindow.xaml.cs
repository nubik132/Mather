using Mather.Data.States;
using Microsoft.Win32;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;

namespace Mather
{
    /// <summary>
    /// Логика взаимодействия для StudentWindow.xaml
    /// </summary>
    public partial class StudentWindow : Window
    {
        Project project;
        public StudentWindow()
        {
            InitializeComponent();
            project = new Project();
            LoadProject();
        }

        private void LoadProject()
        {
            OpenFileDialog dialog = new OpenFileDialog();
            if (dialog.ShowDialog() == true)
            {
                project = StateManager.LoadProject(dialog.FileName);
                LoadStates(project.States);
            }
        }

        public void LoadStates(ObservableCollection<StateBranch> collection)
        {
            StatesTreeView.ItemsSource = collection;
        }

        private void TreeViewItem_Selected(object sender, RoutedEventArgs e)
        {
            if (sender is TreeView tree && tree.SelectedItem is State state)
            {
                DocumentViewer.Document = state.Document;
            }
            e.Handled = true;
        }

        private void OpenProjectMenu_Click(object sender, RoutedEventArgs e)
        {
            LoadProject();
        }
    }
}
