using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;

namespace Mather.Data.States
{
    public abstract class AbstractState
    {
        public string Header { get; set; }
        public AbstractState() { Header = "Заголовок"; }

        public AbstractState(string header)
        {
            Header = header;
        }

        public abstract void Add(AbstractState state);
        public abstract void Remove(AbstractState state);
        public abstract AbstractState GetItem(int position);
    }
}
