using System.Collections.ObjectModel;
using Task = Mather.Data.Tasks.Task;

namespace Mather.Data.States
{
    public class TaskState: AbstractState
    {
        public ObservableCollection<Task> Tasks { get; set; }
        public TaskState(ObservableCollection<Task> tasks): this()
        {
            Tasks = tasks;
        }
        private TaskState()
        {
            Header = "Задание";
        }

        public override void Add(AbstractState state)
        {
        }

        public override void Remove(AbstractState state)
        {
        }

        public override AbstractState GetItem(int position)
        {
            return null;
        }
    }
}
