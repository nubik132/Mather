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

        public override bool Equals(object? obj)
        {
            if (obj is Variable variable) return variable.Name == Name;
            return false;
        }
    }
}
