using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using Mather.Data.Logs;

namespace Mather.Data.Tasks
{
    public abstract class Task : IResultable
    {
        public static readonly double MAX_MARK = 10;
        public FlowDocument Document { get; set; }
        public string Name { get; set; }

        public abstract TaskLog GetLog();
        public abstract double GetResult();
    }
}
