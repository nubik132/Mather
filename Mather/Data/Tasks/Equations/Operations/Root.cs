using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mather.Data.Tasks.Equations.Operations
{
    public class Root : Operation
    {
        public Root(EquationElement left, EquationElement right) : base(left, right) { }

        protected override EquationElement Calculate(Constant left, Constant right)
        {
            return new Constant(Math.Pow(left.Value, 1 / right.Value));
        }

        public override string GetText()
        {
            return $"({Left.GetText()} root {Right.GetText()})";
        }

        public override EquationElement Calculate()
        {
            var leftCalculated = Left.Calculate();
            var rightCalculated = Right.Calculate();

            if (leftCalculated is Constant leftConst && rightCalculated is Constant rightConst)
            {
                return new Constant(Math.Pow(leftConst.Value, 1 / rightConst.Value));
            }

            if (leftCalculated is Variable || rightCalculated is Variable)
            {
                return new Root(leftCalculated, rightCalculated);
            }

            return this;
        }
    }

}
