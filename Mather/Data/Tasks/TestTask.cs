using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;

namespace Mather.Data.Tasks
{
    public class TestTask : Task
    {
        private ObservableCollection<TestTaskElement> checks;
        public TestTask() : this(
            new FlowDocument(new Paragraph(new Run("Новое тестовое задание"))), 
            new ObservableCollection<TestTaskElement>() { new TestTaskElement() }
            ) { }
        public TestTask(FlowDocument document, ObservableCollection<TestTaskElement> checks) 
        {
            Document = document;
            this.checks = checks;
        }

        public override double GetResult()
        {
            double sum = checks.Sum((mark) => mark.GetResult());
            double max = checks.Count * MAX_MARK;
            return sum / max;
        }
    }
}
