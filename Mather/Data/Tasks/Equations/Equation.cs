using Mather.Data.Tasks.Equations.Operations;

namespace Mather.Data.Tasks.Equations
{
    public class Equation : Operation
    {
        public Equation(EquationElement left, EquationElement right) : base(left, right)
        {
        }

        public bool Solve()
        {
            return Math.Abs(((Constant)Left.Calculate()).Value - ((Constant)Right.Calculate()).Value) < 1e-6; // Допустимая погрешность
        }

        public override EquationElement Calculate()
        {
            Left = Left.Calculate();
            Right = Right.Calculate();
            return this;
        }

        public override string GetText()
        {
            return $"{Left.GetText()} = {Right.GetText()}";
        }
    }
}
