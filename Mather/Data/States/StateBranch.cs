using System.Collections.ObjectModel;
using System.Windows.Documents;
using System.Xml.Serialization;

namespace Mather.Data.States
{
    [Serializable]
    [XmlInclude(typeof(State))]
    public class StateBranch : AbstractState
    {
        public ObservableCollection<AbstractState> States { get; set; }

        public StateBranch() : base() { States = new ObservableCollection<AbstractState>(); }
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
