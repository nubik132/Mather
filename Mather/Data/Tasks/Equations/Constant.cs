using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mather.Data.Tasks.Equations
{
    public class Constant : EquationElement
    {
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
    }
}
