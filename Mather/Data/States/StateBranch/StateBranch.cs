using System.Collections.ObjectModel;
using System.Windows.Documents;

namespace Mather.Data.States.StateBranch
{
    public class StateBranch : AbstractState
    {
        public ObservableCollection<AbstractState> States { get; set; }

        public StateBranch() : base() { States = new ObservableCollection<AbstractState>(); Header = "Новая ветвь"; }
        public StateBranch(string header) : base(header)
        {
            States = new ObservableCollection<AbstractState>();
        }

        public override void Add(AbstractState state)
        {
            States.Add(state);
        }

        public override AbstractState GetItem(int position)
        {
            return States[position];
        }

        public override void Remove(AbstractState state)
        {
            States.Remove(state);
        }
    }
}
