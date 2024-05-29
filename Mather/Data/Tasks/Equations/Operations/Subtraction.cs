namespace Mather.Data.Tasks.Equations.Operations
{
    public class Subtraction : Operation
    {
        public Subtraction(EquationElement left, EquationElement right) : base(left, right) { }

        public override EquationElement Calculate()
        {
            Left = Left.Calculate();
            Right = Right.Calculate();
            if (Left.Equals(Right)) return new Constant(0);
            if (Left is Constant leftConst && Right is Constant rightConst)
            {
                return new Constant(leftConst.Value - rightConst.Value);
            }

            return this;
        }

        public override string GetText()
        {
            return $"{Left.GetText()}-{Right.GetText()}";
        }

        public override bool Equals(object? obj)
        {
            if (obj is Subtraction element) return element.Left.Equals(Left) && element.Right.Equals(Right);
            return false;
        }
    }
}
