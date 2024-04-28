﻿using Mather.Data.States;
using Microsoft.Win32;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;

namespace Mather
{
    /// <summary>
    /// Логика взаимодействия для TeacherWindow.xaml
    /// </summary>
    public partial class TeacherWindow : Window
    {
        Project project;

        public TeacherWindow() : this(new Project()) { }

        public TeacherWindow(Project project)
        {
            InitializeComponent();
            this.project = project;
            this.Title = project.Name;
            LoadStates(this.project.States);
            //DocumentEditor.Document = new FlowDocument();
        }

        public void LoadStates(ObservableCollection<StateBranch> collection)
        {
            StatesTreeView.ItemsSource = project.States;
        }

        private void TreeViewItem_Selected(object sender, RoutedEventArgs e)
        {
            if (sender is TreeView tree && tree.SelectedItem is State state)
            {
                DocumentEditor.Document = state.Document;
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
            if (StatesTreeView.SelectedItem is StateBranch selectedBranch)
            {
                SetStateHeader();
                selectedBranch.Add(state);
            }
            else if (StatesTreeView.SelectedItem == null && state is StateBranch branch)
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
            AbstractState? searchedState = StatesTreeView.SelectedItem as AbstractState;
            if (StatesTreeView?.SelectedItem != null)
            {
                foreach (StateBranch branch in project.States)
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

        private void BoldButton_Click(object sender, RoutedEventArgs e)
        {
            var selection = DocumentEditor.Selection;
            if (selection != null)
            {
                var currentWeight = selection.GetPropertyValue(TextElement.FontWeightProperty);
                selection.ApplyPropertyValue(TextElement.FontWeightProperty,
                    (currentWeight.Equals(FontWeights.Bold)) ? FontWeights.Normal : FontWeights.Bold);
            }
        }

        private void ItalicButton_Click(object sender, RoutedEventArgs e)
        {
            var selection = DocumentEditor.Selection;
            if (selection != null)
            {
                var currentStyle = selection.GetPropertyValue(TextElement.FontStyleProperty);
                selection.ApplyPropertyValue(TextElement.FontStyleProperty,
                    (currentStyle.Equals(FontStyles.Italic)) ? FontStyles.Normal : FontStyles.Italic);
            }
        }

        private void UnderlineButton_Click(object sender, RoutedEventArgs e)
        {
            var selection = DocumentEditor.Selection;
            if (selection != null)
            {
                var currentDecorations = selection.GetPropertyValue(Inline.TextDecorationsProperty);
                TextDecorationCollection decorations = currentDecorations as TextDecorationCollection;
                if (decorations != null && decorations.Contains(TextDecorations.Underline[0]))
                {
                    TextDecorationCollection newDecorations = new TextDecorationCollection(decorations);
                    newDecorations.Remove(TextDecorations.Underline[0]);
                    selection.ApplyPropertyValue(Inline.TextDecorationsProperty, newDecorations);
                }
                else
                {
                    selection.ApplyPropertyValue(Inline.TextDecorationsProperty, TextDecorations.Underline);
                }
            }
        }
    }
}
