using Mather.Data.Logs;
using Mather.Data.Tasks.Equations;
using System.Windows.Documents;

namespace Mather.Data.Tasks
{
    public class EquationTask : Task
    {
        public double Answer{ get; set; }
        public Equation Equation { get; set; }
        public EquationTask(FlowDocument document, string name = "Задание") : base(document, name)
        {
            Answer = 1;
            Equation = new Equation(new Variable("x"), new Constant(1));
            
        }
        public EquationTask() : base(DocumentFabric.Custom("Новое уравнение"), "Уравнение")
        {
            Answer = 1;
            Equation = new Equation(new Variable("x"), new Constant(1));
        }
        public override Log GetLog()
        {
            return new EquationLog 
            {
                IsRight = Equation.Equals(Answer), 
                Answer = Answer.ToString(),
                Equation = Equation.GetText()
            };
        }

        public override double GetResult()
        {
            return Equation.Equals(Equation) ? MAX_MARK : 0;
        }
    }
}
