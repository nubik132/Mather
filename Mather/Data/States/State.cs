using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;

namespace Mather.Data.States
{
    public class State : AbstractState
    {
        public FlowDocument Document { get; set; }
        public State() : base() { Document = new FlowDocument(new Paragraph(new Run("Новый документ"))); }
        public State(string header, FlowDocument document) : base(header) { Document = document; }

        public override void Add(AbstractState state) { }

        public override AbstractState GetItem(int position) { throw new Exception("Cannot get child from State!"); }

        public override void Remove(AbstractState state) { }
    }
}
