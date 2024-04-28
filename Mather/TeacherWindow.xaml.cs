using Mather.Data.States;
using Mather.Data.States.StateBranch;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace Mather
{
    /// <summary>
    /// Логика взаимодействия для TeacherWindow.xaml
    /// </summary>
    public partial class TeacherWindow : Window
    {
        Project project;
        public TeacherWindow()
        {
            InitializeComponent();
            project = new Project("Новый", new ObservableCollection<StateBranch>());
            LoadStates(project.States);
        }

        public TeacherWindow(Project project)
        {
            InitializeComponent();
            this.project = project;
            LoadStates(this.project.States);
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

        private void NewGroupStateButton_Click(object sender, RoutedEventArgs e)
        {
            AddState(new StateBranch());
            var newCollection = new ObservableCollection<StateBranch>(project.States);
            LoadStates(newCollection);
        }

        private void NewStateButton_Click(object sender, RoutedEventArgs e)
        {
            AddState(new State());
        }

        private void AddState(AbstractState state)
        {
            if(StatesTreeView.SelectedItem is StateBranch selectedBranch)
            {
                SetStateHeader();
                selectedBranch.Add(state);
            }
            else if(StatesTreeView.SelectedItem == null && state is StateBranch branch)
            {
                SetStateHeader();
                project.States.Add(branch);
            }

            void SetStateHeader()
            {
                NewGroupTeacherWindow window = new NewGroupTeacherWindow();
                if (window.ShowDialog() == true)
                {
                    state.Header = window.GroupName;
                }
            }

        }

        private void DeleteStateButton_Click(object sender, RoutedEventArgs e)
        {
            AbstractState searchedState = StatesTreeView.SelectedItem as AbstractState;
            if (StatesTreeView.SelectedItem != null)
            {
                foreach(StateBranch branch in project.States)
                {
                    if (searchedState == branch)
                    {
                        project.States.Remove(branch);
                        return;
                    }
                    Cycle(branch);
                }
            }

            void Cycle(StateBranch branch)
            {
                foreach (AbstractState abstractState in branch.States)
                {
                    if (searchedState == abstractState)
                    {
                        branch.Remove(searchedState);
                        return;
                    }
                    if (abstractState is StateBranch stateBranch)
                    {
                        Cycle(stateBranch);
                    }
                }
            }
        }

        private void NewProjectMenu_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog();
            if (dialog.ShowDialog() == true)
            {
                project = new Project();
                StateManager.SaveProject(project, dialog.FileName);
                LoadStates(project.States);
            }
        }

        private void OpenProjectMenu_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            if (dialog.ShowDialog() == true)
            {
                project = StateManager.LoadProject(dialog.FileName);
                LoadStates(project.States);
            }
        }

        private void SaveProjectMenu_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog();
            if (dialog.ShowDialog() == true)
            {
                StateManager.SaveProject(project, dialog.FileName);
                LoadStates(project.States);
            }
        }
    }
}
