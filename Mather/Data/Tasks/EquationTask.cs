using Mather.Data.Logs;
using Mather.Data.Tasks.Equations;
using System.Windows.Documents;

namespace Mather.Data.Tasks
{
    public class EquationTask : Task
    {
        public Equation AnswerEquation { get; set; }
        public Equation UserEquation { get; set; }
        public EquationTask(FlowDocument document, string name = "Задание") : base(document, name)
        {
            AnswerEquation = new Equation(new Variable("x"), new Constant(1));
            UserEquation = new Equation(new Variable("x"), new Constant(1));
        }

        public override Log GetLog()
        {
            throw new NotImplementedException();
        }

        public override double GetResult()
        {
            throw new NotImplementedException();
        }
    }
}
