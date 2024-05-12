using System.Collections.ObjectModel;
using Task = Mather.Data.Tasks.Task;

namespace Mather.Data.States
{
    public class TaskState: AbstractState
    {
        public ObservableCollection<Task> Tasks { get; set; }
        public TaskState(ObservableCollection<Task> tasks, string header): base(header)
        {
            Tasks = tasks;
        }
        public TaskState()
        {
            Header = "Задание";
            Tasks = new ObservableCollection<Task>();
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
