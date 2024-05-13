using Mather.Data.States;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;

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
            XamlManager.Save(branch, path);
            var newState = XamlManager.Load<StateBranch>(path);
            Assert.IsTrue(newState != null);
        }
        [WpfTestMethod]
        public void TestSaveLoadInWindow()
        {
            XamlManager.Save(branch, path);
            var newState = XamlManager.Load<StateBranch>(path);
            Assert.IsTrue(newState != null);
            var collection = new ObservableCollection<StateBranch>
            {
                newState
            };
            window.LoadStates(collection);
            window.Show();
            Assert.IsTrue(window.StatesTreeView.ItemsSource as ObservableCollection<StateBranch> == collection);
            //Assert.IsTrue(true);
        }
        [WpfTestMethod]
        public void TestSaveLoadProject()
        {
            Project project = new Project("Проект 1", new ObservableCollection<StateBranch>
            {
                branch
            });
            XamlManager.Save(project, path);
            var newProject = XamlManager.Load<Project>(path);
            Assert.IsTrue(newProject != null);
        }
        [WpfTestMethod]
        public void TestSaveLoadProjectWithTwoBranches()
        {
            var newBranch = new StateBranch("b1");
            State state1 = new("s1", new FlowDocument()), state2 = new("s2", new FlowDocument()), state3 = new("s3", new FlowDocument());
            newBranch.Add(state1);
            newBranch.Add(state2);
            newBranch.Add(state3);
            Project project = new Project("Проект 1", new ObservableCollection<StateBranch>
            {
                branch, newBranch
            });
            XamlManager.Save(project, path);
            var newProject = XamlManager.Load<Project>(path);
            Assert.AreEqual(newProject.Name, project.Name);
            Assert.AreEqual(branch.Header, newProject.States[0].Header);
            Assert.AreEqual(newBranch.Header, newProject.States[1].Header);
            Assert.AreEqual(branch.States[0].Header, newProject.States[0].States[0].Header);
            Assert.AreEqual(newBranch.States[0].Header, newProject.States[1].States[0].Header);
            Assert.AreEqual(newBranch.States[1].Header, newProject.States[1].States[1].Header);
            Assert.AreEqual(newBranch.States[2].Header, newProject.States[1].States[2].Header);
        }
        [WpfTestMethod]
        public void TestSaveLoadProjectWithTwoBranchesAndInnerBranch()
        {
            var newBranch = new StateBranch("b1");
            var innerBranch = new StateBranch("ib1");

            State state1 = new("s1", new FlowDocument()), state2 = new("s2", new FlowDocument()), state3 = new("s3", new FlowDocument()), state4 = new("s4", new FlowDocument());
            newBranch.Add(state1);
            newBranch.Add(state2);
            newBranch.Add(state3);
            innerBranch.Add(state4);
            newBranch.Add(innerBranch);
            Project project = new Project("Проект 1", new ObservableCollection<StateBranch>
            {
                branch, newBranch
            });
            XamlManager.Save(project, path);
            var newProject = XamlManager.Load<Project>(path);
            Assert.AreEqual(newProject.Name, project.Name);
            Assert.AreEqual(branch.Header, newProject.States[0].Header);
            Assert.AreEqual(newBranch.Header, newProject.States[1].Header);
            Assert.AreEqual(branch.States[0].Header, newProject.States[0].States[0].Header);
            Assert.AreEqual(newBranch.States[0].Header, newProject.States[1].States[0].Header);
            Assert.AreEqual(newBranch.States[1].Header, newProject.States[1].States[1].Header);
            Assert.AreEqual(newBranch.States[2].Header, newProject.States[1].States[2].Header);
            Assert.AreEqual(innerBranch.Header, newProject.States[1].States[3].Header);
            Assert.AreEqual(innerBranch.States[0].Header, ((StateBranch)newProject.States[1].States[3]).States[0].Header);
        }
    }
}
