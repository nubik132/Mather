using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;

namespace Mather.Data.Tasks
{
    public abstract class Task : ITaskElement
    {
        public static readonly double MAX_MARK = 10;
        public FlowDocument Document { get; set; }
        public abstract double GetResult();
    }
}
