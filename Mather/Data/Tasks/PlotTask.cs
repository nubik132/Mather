using Mather.Data.Logs;
using Mather.Data.Tasks.Graphics;
using System.Collections.ObjectModel;
using System.Windows.Documents;

namespace Mather.Data.Tasks
{
    public class PlotTask : Task
    {
        public CoordinatePlane AnswerPlane { get; set; }
        public CoordinatePlane UserPlane { get; set; }
        public PlotTask(FlowDocument document, string name = "Задание") : base(document, name)
        {
            AnswerPlane = new CoordinatePlane();
            UserPlane = new CoordinatePlane();
            //UserPlane.Center = new System.Windows.Point(1, 2);
        }
        public override Log GetLog()
        {
            return AnswerPlane.GetLog();
        }

        public override double GetResult()
        {
            if (AnswerPlane.ComparePlots(UserPlane))
            {
                return MAX_MARK;
            }

            // TODO: remake code at bottom to calculating by logs


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
