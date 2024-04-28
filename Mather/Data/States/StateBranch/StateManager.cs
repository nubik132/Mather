using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Markup;

namespace Mather.Data.States.StateBranch
{
    public static class StateManager
    {
        public static void Save(AbstractState state, string path)
        {
            using FileStream fs = File.Open(path, FileMode.Create);

            XamlWriter.Save(state, fs);
        }
        public static AbstractState Load(string path)
        {
            using FileStream fs = File.Open(path, FileMode.Open);

            AbstractState? document = XamlReader.Load(fs) as AbstractState;
            return document ?? throw new FileNotFoundException();
        }
    }
}
