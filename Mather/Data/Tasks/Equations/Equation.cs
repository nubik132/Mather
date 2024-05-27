using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mather.Data.Tasks.Equations
{
    public class Equation
    {
        public EquationElement Left { get; set; }
        public EquationElement Right { get; set; }

        public Equation(EquationElement left, EquationElement right)
        {
            Left = left;
            Right = right;
        }

        public bool Solve()
        {
            return Math.Abs(((Constant)Left.Calculate()).Value - ((Constant)Right.Calculate()).Value) < 1e-6; // Допустимая погрешность
        }

        public string GetText()
        {
            return $"{Left.GetText()} = {Right.GetText()}";
        }
    }
}
