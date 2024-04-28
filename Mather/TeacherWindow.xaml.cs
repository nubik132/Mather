using Mather.Data.States.StateBranch;
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
        ObservableCollection<StateBranch> states;
        public TeacherWindow()
        {
            InitializeComponent();
            states = new ObservableCollection<StateBranch>();
            LoadStates(states);
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
                states.Add(branch);
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
                foreach(StateBranch branch in states)
                {
                    if (searchedState == branch)
                    {
                        states.Remove(branch);
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
    }
}
