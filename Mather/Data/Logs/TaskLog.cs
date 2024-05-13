using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using System.Windows.Media;

namespace Mather.Data.Logs
{
    public class TaskLog
    {
        private static readonly SolidColorBrush greenBrush = new SolidColorBrush(Colors.Green);
        private static readonly SolidColorBrush redBrush = new SolidColorBrush(Colors.Red);
        private ObservableCollection<LogElement> Logs { get; set; }
        public TaskLog(IEnumerable<LogElement> logs)
        {
            Logs = new ObservableCollection<LogElement>(logs);
        }
        public Paragraph ToParagraph()
        {
            Paragraph paragraph = new Paragraph();
            foreach (var log in Logs)
            {
                Run run = new Run(log.ToString());
                run.Foreground = log.IsRight ? greenBrush : redBrush;
                paragraph.Inlines.Add(run);
                paragraph.Inlines.Add(new LineBreak());
            }
            return paragraph;
        }
    }
}
