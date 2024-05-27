using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mather.Data.Tasks.Equations
{
    public class Variable : EquationElement
    {
        public string Name { get; set; }

        public Variable(string name)
        {
            Name = name;
        }

        public override EquationElement Calculate()
        {
            return this;
        }

        public override string GetText()
        {
            return Name;
        }
    }
}
