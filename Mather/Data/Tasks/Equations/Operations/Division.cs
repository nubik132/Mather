using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mather.Data.Tasks.Equations.Operations
{
    public class Division : Operation
    {
        public Division(EquationElement left, EquationElement right) : base(left, right) { }

        protected override EquationElement Calculate(Constant left, Constant right)
        {
            return left / right;
        }

        public override string GetText()
        {
            return $"({Left.GetText()} / {Right.GetText()})";
        }

        public override EquationElement Calculate()
        {
            var leftCalculated = Left.Calculate();
            var rightCalculated = Right.Calculate();

            if (leftCalculated is Constant leftConst && rightCalculated is Constant rightConst)
            {
                return leftConst / rightConst;
            }

            if (leftCalculated is Variable leftVar && rightCalculated is Variable rightVar && leftVar.Name == rightVar.Name)
            {
                return new Constant(1);
            }

            if (leftCalculated is Multiplication leftMult && rightCalculated is Variable rightVar2)
            {
                if (leftMult.Right is Variable leftMultRightVar && leftMultRightVar.Name == rightVar2.Name)
                {
                    return leftMult.Left;
                }
            }

            if (leftCalculated is Multiplication leftMult2 && rightCalculated is Multiplication rightMult)
            {
                if (leftMult2.Right is Variable leftMult2RightVar && rightMult.Right is Variable rightMultRightVar && leftMult2RightVar.Name == rightMultRightVar.Name)
                {
                    return leftMult2.Left / rightMult.Left;
                }
            }

            return this;
        }
    }

}
