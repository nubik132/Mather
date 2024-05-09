using Mather.Data.States;
using System.Windows;
using System.Windows.Controls;
using Task = Mather.Data.Tasks.Task;

namespace Mather.Data
{
    public class StateTaskSelector : DataTemplateSelector
    {
        public DataTemplate AbstractStateTemplate { get; set; }
        public DataTemplate TaskTemplate { get; set; }

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            if (item is TaskState)
            {
                return TaskTemplate;
            }
            if (item is AbstractState)
            {
                return AbstractStateTemplate;
            }
            else 

            return base.SelectTemplate(item, container);
        }
    }
}
