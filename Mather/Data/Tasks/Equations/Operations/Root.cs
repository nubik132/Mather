namespace Mather.Data.Tasks.Equations.Operations
{
    public class Root : Operation
    {
        public Root(EquationElement left) : base(left, new Constant(2)) { }
        public Root(EquationElement left, EquationElement right) : base(left, right) { }

        public override string GetText()
        {
            return $"\\sqrt{{{Left.GetText()}}}";
        }

        public override EquationElement Calculate()
        {
            Left = Left.Calculate();
            Right = Right.Calculate();

            if (Left is Constant leftConst && Right is Constant rightConst)
            {
                return new Constant(Math.Pow(leftConst.Value, 1.0 / rightConst.Value));
            }

            return this;
        }

        public override bool Equals(object? obj)
        {
            if (obj is Root element) return element.Left.Equals(Left) && element.Right.Equals(Right);
            return false;
        }
    }
}
