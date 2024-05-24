using Mather.Data;
using Mather.Data.States;
using Mather.Data.Tasks;
using Mather.Data.Tasks.Graphics;
using Mather.Windows;
using Microsoft.Win32;
using System.Collections.ObjectModel;
using System.IO;
using System.Reflection.Metadata;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using Task = Mather.Data.Tasks.Task;

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
            var stateBranch = new StateBranch("Графики");
            var plane = new CoordinatePlane
            {
                Size = 15,
            };
            plane.Plots.Add(new LinePlot(plane, 1, 2));
            stateBranch.States.Add(new TaskState(new ObservableCollection<Task>
            {
                new PlotTask(new FlowDocument(), "График")
            },
                "График"));
            project.States.Add(stateBranch);
            LoadStates(this.project.States);
        }

        public void LoadStates(ObservableCollection<StateBranch> collection)
        {
            StatesTreeView.ItemsSource = collection;
        }

        private void TreeViewItem_Selected(object sender, RoutedEventArgs e)
        {
            if (StatesTreeView.SelectedItem is State state)
            {
                DocumentEditor.Document = state.Document;
                TaskTab.Visibility = Visibility.Hidden;
            }
            else if (StatesTreeView.SelectedItem is Task task)
            {
                DocumentEditor.Document = task.Document;
                TaskTab.Visibility = Visibility.Visible;
                TaskControl.Content = task;
                TaskControl.UpdateLayout();
            }
            e.Handled = true;
        }

        private void NewGroupStateButton_Click(object sender, RoutedEventArgs e)
        {
            AddState(new StateBranch());
        }

        private void NewStateButton_Click(object sender, RoutedEventArgs e)
        {
            if (StatesTreeView.SelectedItem is StateBranch)
                AddState(new State());
            else if (StatesTreeView.SelectedItem is TaskState taskState)
                taskState.Tasks.Add(CreateTask());
        }

        private void AddState(AbstractState state)
        {
            if (StatesTreeView.SelectedItem is StateBranch selectedBranch)
            {
                string header = CreateHeader();

                if (header == string.Empty) return;

                state.Header = header;
                selectedBranch.Add(state);
            }
            else if (StatesTreeView.SelectedItem == null && state is StateBranch branch)
            {
                string header = CreateHeader();

                if (header == string.Empty) return;

                state.Header = header;
                project.States.Add(branch);
            }
        }

        public string CreateHeader()
        {
            NewGroupTeacherWindow window = new NewGroupTeacherWindow();
            if (window.ShowDialog() == true)
            {
                return window.Name;
            }
            return string.Empty;
        }
        private Task CreateTask()
        {
            AddTaskWindow window = new AddTaskWindow();
            if(window.ShowDialog() == true)
            {
                switch (window.SelectedTask)
                {
                    case AddTaskWindow.Tasks.Test:
                        return new TestTask();
                    case AddTaskWindow.Tasks.Plot:
                        return new PlotTask(DocumentFabric.Custom("Новый график")); 
                    case AddTaskWindow.Tasks.Equation:
                        throw new NotImplementedException(); break;
                }
            }
            throw new Exception("Не удалось создать задание");
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
                XamlManager.Save(project, dialog.FileName);
                LoadStates(project.States);

            }
        }

        private void OpenProjectMenu_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            if (dialog.ShowDialog() == true)
            {
                project = XamlManager.Load<Project>(dialog.FileName);
                LoadStates(project.States);
            }
        }

        private void SaveProjectMenu_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog();
            if (dialog.ShowDialog() == true)
            {
                XamlManager.Save(project, dialog.FileName);
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

        private void OpenTaskMenu_Click(object sender, RoutedEventArgs e)
        {
            if (StatesTreeView.SelectedItem is TaskState taskState)
            {
                DocumentEditor.Document = new FlowDocument();
                TaskWindow window = new TaskWindow(taskState);
                window.ShowDialog();
            }

        }

        private void OpenLogMenu_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new OpenFileDialog();
            //dialog.Filter = ".xlg";
            dialog.DefaultExt = "xlg";
            string path = Environment.ExpandEnvironmentVariables(@"%appdata%\Mather\Logs");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            dialog.InitialDirectory = path;
            var dialogContent = dialog.ShowDialog();
            if (dialogContent != null && dialogContent == true)
            {
                FlowDocument document = XamlManager.Load<FlowDocument>(dialog.FileName);
                LogWindow window = new LogWindow(document);
                window.ShowDialog();
            }
        }
    }
}
