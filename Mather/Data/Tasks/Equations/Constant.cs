using System.Xml.Linq;

namespace Mather.Data.Tasks.Equations
{
    public class Constant : EquationElement
    {
        public static Constant One { get; } = new Constant(1);
        public double Value { get; set; }

        public Constant(double value)
        {
            Value = value;
        }

        public override EquationElement Calculate()
        {
            return this;
        }

        public override string GetText()
        {
            return Value.ToString();
        }

        public override bool Equals(object? obj)
        {
            if (obj is Constant constant) return constant.Value == Value;
            return false;
        }
    }
}
