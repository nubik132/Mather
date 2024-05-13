using Mather.Data.States;
using Mather.Data.Tasks;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Task = Mather.Data.Tasks.Task;

namespace MatherTest
{
    [TestClass]
    public class TaskTesting
    {
        TeacherWindow window;
        [TestInitialize]
        public void Initialize()
        {
            window = new TeacherWindow();
            Task task = new TestTask();
            window.LoadStates(new ObservableCollection<StateBranch>() {
                new StateBranch()
                {
                    States = new ObservableCollection<AbstractState>() 
                    { 
                        new TaskState(new ObservableCollection<Task>() { task }, "TestTask") 
                    }
                }
            });
            window.Show();
        }

        [WpfTestMethod]
        public void Test()
        {
            var branch = (ObservableCollection<StateBranch>)window.StatesTreeView.ItemsSource;
            //var type = typeof(window.StatesTreeView.ItemsSource);
            Assert.IsNotNull(window.StatesTreeView.ItemsSource);
        }
    }
}
