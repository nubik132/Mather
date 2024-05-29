namespace Mather.Data.Tasks.Equations.Operations
{
    public class Addition : Operation
    {
        public Addition(EquationElement left, EquationElement right) : base(left, right) { }

        public override EquationElement Calculate()
        {
            Left = Left.Calculate();
            Right = Right.Calculate();

            if (Left is Constant leftConst && Right is Constant rightConst)
            {
                return new Constant(leftConst.Value + rightConst.Value);
            }

            return this;
        }

        public override string GetText()
        {
            return $"{Left.GetText()}+{Right.GetText()}";
        }

        public override bool Equals(object? obj)
        {
            if (obj is Addition element) return element.Left.Equals(Left) && element.Right.Equals(Right);
            return false;
        }
    }
}
