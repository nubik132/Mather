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
        public ObservableCollection<TestTaskElement> Checks { get; set; }
        public TestTask() : this(
            new FlowDocument(new Paragraph(new Run("Новое тестовое задание"))), 
            new ObservableCollection<TestTaskElement>()
            ) 
        {
            Name = "Тестовое задание";
        }
        public TestTask(FlowDocument document, ObservableCollection<TestTaskElement> checks, string name = "Тестовое задание") 
        {
            Document = document;
            Checks = checks;
            Name = name;
        }

        public override double GetResult()
        {
            double sum = Checks.Sum((mark) => mark.GetResult());
            double max = Checks.Count * MAX_MARK;
            return Math.Round(sum / max * MAX_MARK, 2);
        }
    }
}
