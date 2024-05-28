using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mather.Data.Tasks.Equations.Operations
{
    public class Subtraction : Operation
    {
        public Subtraction(EquationElement left, EquationElement right) : base(left, right) { }

        protected override EquationElement Calculate(Constant left, Constant right)
        {
            return left - right;
        }

        public override string GetText()
        {
            return $"{Left.GetText()}-{Right.GetText()}";
        }
    }
}
