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
    /// Логика взаимодействия для StudentWindow.xaml
    /// </summary>
    public partial class StudentWindow : Window
    {
        public StudentWindow()
        {
            InitializeComponent();
            var collection = new ObservableCollection<StateBranch>();
            var branch = new StateBranch("Branch 1");
            var state = new State();
            state.Header = "State 1";
            var state2 = new State("State 2", new FlowDocument(new Paragraph(new Run("Тут другой текст.\n Это ж другая статья!!!!"))));
            branch.Add(state);
            branch.Add(state2);
            collection.Add(branch);
            LoadStates(collection);
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
    }
}
