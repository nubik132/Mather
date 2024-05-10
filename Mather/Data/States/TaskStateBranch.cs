using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;

namespace Mather.Data.States
{
    public class TaskStateBranch : StateBranch
    {
        public override void Add(AbstractState state)
        {
            if (state is TaskState taskState)
                States.Add(taskState);
        }

        public override TaskState GetItem(int position)
        {
            return States[position] as TaskState;
        }

        public override void Remove(AbstractState state)
        { 
            States.Remove(state);
        }
    }
}
