using Mather.Data.States.StateBranch;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mather.Data.States
{
    public class Project
    {
        public string Name { get; set; }
        public ObservableCollection<StateBranch.StateBranch> States { get; set; }
        public Project()
        {
            Name = "Новый проект";
            States = new ObservableCollection<StateBranch.StateBranch>();
        }

        public Project(ObservableCollection<StateBranch.StateBranch> states)
        {
            Name = "Новый проект";
            States = states;
        }

        public Project(string name, ObservableCollection<StateBranch.StateBranch> states)
        {
            Name = name;
            States = states;
        }
    }
}
