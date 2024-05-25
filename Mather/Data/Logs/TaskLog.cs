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
    public class TaskLog : Log
    {
        private static readonly SolidColorBrush greenBrush = new SolidColorBrush(Colors.Green);
        private static readonly SolidColorBrush redBrush = new SolidColorBrush(Colors.Red);
        public ObservableCollection<LogElement> Logs { get; set; }
        public TaskLog(IEnumerable<LogElement> logs)
        {
            Logs = new ObservableCollection<LogElement>(logs);
        }
        public TaskLog()
        {
            Logs = new ObservableCollection<LogElement>();
        }
        public override Paragraph ToParagraph()
        {
            Paragraph paragraph = new Paragraph();

            if (!string.IsNullOrEmpty(PreText))
            {
                paragraph.Inlines.Add(PreText);
                paragraph.Inlines.Add(new LineBreak());
            }

            foreach (var log in Logs)
            {
                Run run = new Run(log.ToString());
                run.Foreground = log.IsRight ? greenBrush : redBrush;
                paragraph.Inlines.Add(run);
                paragraph.Inlines.Add(new LineBreak());
            }

            if (!string.IsNullOrEmpty(PostText))
            {
                paragraph.Inlines.Add(PostText);
                paragraph.Inlines.Add(new LineBreak());
            }

            return paragraph;
        }
    }
}
