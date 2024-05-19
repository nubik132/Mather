using Mather.Data.Logs;
using Mather.Data.Tasks.Graphics;
using System.Collections.ObjectModel;
using System.Windows.Documents;

namespace Mather.Data.Tasks
{
    public class PlotTask : Task
    {
        public PlotTask(FlowDocument document, CoordinatePlane answer, string name = "Задание") : base(document, name)
        {
            AnswerPlane = answer;
            UserPlane = new CoordinatePlane();
        }

        public CoordinatePlane AnswerPlane { get; set; }
        public CoordinatePlane UserPlane { get; set; }

        public override TaskLog GetLog()
        {
            List<LogElement> logs = new List<LogElement>();
            logs.Add(new LogElement());
            return new TaskLog(logs);
        }

        public override double GetResult()
        {
            if (AnswerPlane.Plots.Count != UserPlane.Plots.Count) return 0;
            double mark = 0;

            if (UserPlane.Plots.Count == 0) return 0;

            var answerArray = AnswerPlane.Plots.ToArray();
            var userArray = UserPlane.Plots.ToArray();

            Array.Sort(answerArray);
            Array.Sort(userArray);

            for (int i = 0; i < answerArray.Length; i++)
                mark += answerArray[i].Equals(userArray[i]) ? MAX_MARK : 0;

            return Math.Round(mark / answerArray.Length, 2);
        }
    }
}
