using Mather.Data.States;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace MatherTest
{
    [TestClass]
    public class AddStateInTree
    {
        StateBranch branch;
        State state;
        ObservableCollection<StateBranch> collection;
        StudentWindow window;
        Project project;
        [TestInitialize]
        public void Init()
        {
            collection = new ObservableCollection<StateBranch>();
            branch = new StateBranch("Branch 1");
            state = new State();
            state.Header = "State 1";
            project = new Project("Тест проект", collection);
            branch.Add(state);
            //window = new StudentWindow(project);
            //window.Show();
        }

        [WpfTestMethod]
        
        public void LoadfileStudent()
        {
            XamlManager.Save(project, "");
        }
        [WpfTestMethod]
        public void ShowProject()
        {
            Assert.IsTrue(true);
        }

        [TestCleanup]
        public void Cleanup()
        {
            window.Close();
        }
    }
}
