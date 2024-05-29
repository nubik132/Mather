namespace Mather.Data.Tasks.Equations.Operations
{
    public class Pow : Operation
    {
        public Pow(EquationElement left, EquationElement right) : base(left, right) { }

        public override EquationElement Calculate()
        {
            Left = Left.Calculate();
            Right = Right.Calculate();

            if (Right.Equals(Constant.One)) { return Left; }
            if (Left is Constant leftConst && Right is Constant rightConst)
            {
                return new Constant(Math.Pow(leftConst.Value, rightConst.Value));
            }
            return this;
        }

        public override string GetText()
        {
            return $"{Left.GetText()}^{Right.GetText()}";
        }

        public override bool Equals(object? obj)
        {
            if (obj is Pow element) return element.Left.Equals(Left) && element.Right.Equals(Right);
            return false;
        }
    }

}
