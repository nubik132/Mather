using Mather.Data.States.StateBranch;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatherTest
{
    [TestClass]
    public class StateManagerTests
    {
        StateBranch branch;
        State state;
        TeacherWindow window;
        string path = "C:\\Users\\miste\\OneDrive\\Рабочий стол\\doc.xaml";
        [TestInitialize]
        public void Init()
        {
            branch = new StateBranch("Branch 1");
            state = new State();
            state.Header = "State 1";
            branch.Add(state);
            window = new TeacherWindow();
        }

        [WpfTestMethod]
        public void TestSaveLoad()
        {
            StateManager.Save(branch, path);
            var newState = StateManager.Load(path);
            Assert.IsTrue(newState != null);
        }
        [WpfTestMethod]
        public void TestSaveLoadInWindow()
        {
            StateManager.Save(branch, path);
            var newState = StateManager.Load(path);
            Assert.IsTrue(newState != null);
            var collection = new ObservableCollection<StateBranch>
            {
                (StateBranch)newState
            };
            window.LoadStates(collection);
            window.Show();
            Assert.IsTrue(window.StatesTreeView.ItemsSource as ObservableCollection<StateBranch> == collection);
        }
    }
}
