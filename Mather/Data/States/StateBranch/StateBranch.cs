using System.Windows.Documents;

namespace Mather.Data.States.StateBranch
{
    public class StateBranch : AbstractState
    {
        public List<AbstractState> States;

        public StateBranch() : base() { States = new List<AbstractState>(); Header = "Новая ветвь"; }
        public StateBranch(string header) : base(header)
        {
            States = new List<AbstractState>();
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
