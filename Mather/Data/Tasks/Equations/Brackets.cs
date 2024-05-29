namespace Mather.Data.Tasks.Equations
{
    public class Brackets : EquationElement
    {
        public EquationElement Value { get; set; }
        public Brackets(EquationElement element)
        {
            Value = element;
        }

        public override EquationElement Calculate()
        {
            if(Value is Constant) return Value;
            return Value.Calculate();
        }

        public override string GetText()
        {
            return $"({Value.GetText()})";
        }

        public override bool Equals(object? obj)
        {
            if (obj is Brackets brackets) return brackets.Value.Equals(Value);
            return false;
        }
    }
}
