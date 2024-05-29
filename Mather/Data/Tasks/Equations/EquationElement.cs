using Mather.Data.Tasks.Equations.Operations;

namespace Mather.Data.Tasks.Equations
{
    public abstract class EquationElement
    {
        public abstract EquationElement Calculate();
        public abstract string GetText();

        public static EquationElement operator +(EquationElement left, EquationElement right)
        {
            return new Addition(left, right);
        }

        public static EquationElement operator -(EquationElement left, EquationElement right)
        {
            return new Subtraction(left, right);
        }

        public static EquationElement operator *(EquationElement left, EquationElement right)
        {
            return new Multiplication(left, right);
        }

        public static EquationElement operator /(EquationElement left, EquationElement right)
        {
            return new Division(left, right);
        }
    }
}
