using Mather.Data.States;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatherTest
{
    [TestClass]
    public class AddStateInTree
    {
        StateBranch branch;
        State state;
        ObservableCollection<StateBranch> collection;
        StudentWindow window;
        [TestInitialize]
        public void Init()
        {
            collection = new ObservableCollection<StateBranch>();
            branch = new StateBranch("Branch 1");
            state = new State();
            state.Header = "State 1";
            branch.Add(state);
            window = new StudentWindow();
            window.Show();
        }

        [WpfTestMethod]
        
        public void AddBranch()
        {
            collection.Add(branch);
            window.LoadStates(collection);
            window.LoadStates(collection);
            Assert.AreEqual(collection.AsEnumerable(), window.StatesTreeView.ItemsSource);
        }
        [WpfTestMethod]
        public void AddStateAndBranch()
        {
            branch.Add(state);
            collection.Add(branch);
            window.LoadStates(collection);
            Assert.AreEqual(collection.AsEnumerable(), window.StatesTreeView.ItemsSource);
        }

        [TestCleanup]
        public void Cleanup()
        {
            window.Close();
        }
    }
}
