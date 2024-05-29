namespace Mather.Data.Tasks.Equations.Operations
{
    public abstract class Operation : EquationElement
    {
        public EquationElement Left { get; set; }
        public EquationElement Right { get; set; }

        protected Operation(EquationElement left, EquationElement right)
        {
            Left = left;
            Right = right;
        }
    }
}
