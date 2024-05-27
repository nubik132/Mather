using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mather.Data.Tasks.Equations.Operations
{
    public abstract class Operation : EquationElement
    {
        public EquationElement Left { get; set; }
        public EquationElement Right { get; set; }

        protected Operation(EquationElement left, EquationElement right)
        {
            Left = left;
            Right = right;
        }

        protected EquationElement Simplify()
        {
            var leftCalculated = Left.Calculate();
            var rightCalculated = Right.Calculate();

            if (leftCalculated is Constant leftConst && rightCalculated is Constant rightConst)
            {
                return Calculate(leftConst, rightConst);
            }

            if (leftCalculated is Variable leftVar && rightCalculated is Variable rightVar && leftVar.Name == rightVar.Name)
            {
                return new Constant(1);
            }

            if (leftCalculated is Operation || rightCalculated is Operation)
            {
                return this;
            }

            return this;
        }

        protected abstract EquationElement Calculate(Constant left, Constant right);

        public override EquationElement Calculate()
        {
            return Simplify();
        }
    }
}
