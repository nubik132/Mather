using Mather.Data.Tasks.Equations.Operations;

namespace Mather.Data.Tasks.Equations
{
    public class Equation : Operation
    {
        public Equation(EquationElement left, EquationElement right) : base(left, right)
        {
        }
        public Equation() : base(new Variable("x"), new Constant(1)) { }
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

        public override bool Equals(object? obj)
        {
            if ((obj is Equation equation) &&
                ((Left.Equals(equation.Left) && Right.Equals(equation.Right)) ||
                Left.Equals(equation.Right) && Right.Equals(equation.Left)))
            {
                return true;
            }
            if ((obj is double answer) &&
                (Left is Constant && (Left as Constant).Value == answer && Right is Variable ||
                Right is Constant && (Right as Constant).Value == answer && Left is Variable))
            {
                return true;
            }
            return false;
        }
    }
}
