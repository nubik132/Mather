using System.Windows;

namespace Mather.Data.Tasks
{
    public class TestTaskElement : DependencyObject, ITaskElement
    {
        public static readonly DependencyProperty CheckProperty;
        public bool Answer { get; }
        public string Text { get; set; }

        static TestTaskElement()
        {
            CheckProperty = DependencyProperty.Register("Check", typeof(bool), typeof(TestTaskElement));
        }
        public TestTaskElement(bool answer = false, string text = "???") 
        {
            Answer = answer;
            Check = false;
            Text = text;
        }
        public bool Check
        {
            get { return (bool)GetValue(CheckProperty); }
            set { SetValue(CheckProperty, value); }
        }

        public double GetResult()
        {
            return Check == Answer ? 10 : 0;
        }
    }
}
