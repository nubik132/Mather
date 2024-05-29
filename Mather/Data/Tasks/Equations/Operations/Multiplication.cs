namespace Mather.Data.Tasks.Equations.Operations
{
    public class Multiplication : Operation
    {
        public Multiplication(EquationElement left, EquationElement right) : base(left, right) { }

        public override EquationElement Calculate()
        {
            Left = Left.Calculate();
            Right = Right.Calculate();

            if (Left is Constant leftConst && Right is Constant rightConst)
            {
                return new Constant(leftConst.Value * rightConst.Value);
            }
            if (Left is Variable && Left.Equals(Right))
            {
                return new Pow(Left, new Constant(2));
            }
            if (Left is Pow powL && Right.Equals(powL.Left))
            {
                return new Pow(powL.Left, powL.Right + new Constant(1));
            }
            if (Right is Pow powR && Left.Equals(powR.Left))
            {
                return new Pow(powR.Left, powR.Right + new Constant(1));
            }
            if (Left is Pow powLeft && Right is Pow powRight && powLeft.Left.Equals(powRight.Right))
            {
                return new Pow(powLeft.Left, powRight.Right + powRight.Right);
            }
            return this;
        }

        public override string GetText()
        {
            return $"{Left.GetText()} \\cdot {Right.GetText()}";
        }

        public override bool Equals(object? obj)
        {
            if (obj is Multiplication element) return element.Left.Equals(Left) && element.Right.Equals(Right);
            return false;
        }
    }
}
