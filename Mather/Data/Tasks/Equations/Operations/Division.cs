namespace Mather.Data.Tasks.Equations.Operations
{
    public class Division : Operation
    {
        public Division(EquationElement left, EquationElement right) : base(left, right) { }

        public override string GetText()
        {
            return $"\\frac{{{Left.GetText()}}}{{{Right.GetText()}}}";
        }

        public override EquationElement Calculate()
        {
            Left = Left.Calculate();
            Right = Right.Calculate();

            if (Left is Constant leftConst && Right is Constant rightConst)
            {
                return new Constant(leftConst.Value / rightConst.Value);
            }
            if (Left is Pow pow && pow.Left.Equals(Right))
            {
                pow.Right = pow.Right - new Constant(1);

                if (Right is Pow powRight) powRight.Right = powRight.Right - new Constant(1);
                else return Left;

            }
            if (Right.Equals(Constant.One))
            {
                return Left;
            }
            if (Left.Equals(Right))
            {
                return new Constant(1);
            }
            return this;
        }

        public override bool Equals(object? obj)
        {
            if (obj is Division element) return element.Left.Equals(Left) && element.Right.Equals(Right);
            return false;
        }
    }

}
