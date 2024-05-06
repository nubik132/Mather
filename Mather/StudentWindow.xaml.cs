using Mather.Data.States;
using Microsoft.Win32;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace Mather
{
    /// <summary>
    /// Логика взаимодействия для StudentWindow.xaml
    /// </summary>
    public partial class StudentWindow : Window
    {
        public Project project;
        public StudentWindow() : this(LoadProject()) { }

        public StudentWindow(Project project)
        {
            InitializeComponent();
            this.project = project;
            LoadStates(this.project.States);
        }

        private static Project LoadProject()
        {
            OpenFileDialog dialog = new OpenFileDialog();
            if (dialog.ShowDialog() == true)
            {
                Project project;
                project = StateManager.LoadProject(dialog.FileName);
                return project;
            }
            throw new Exception("Не удалось загрузить проект");
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
