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
        public static void Save(FlowDocument document, string path)
        {
            using FileStream fs = File.Open(path, FileMode.Create);

            XamlWriter.Save(document, fs);
        }
        public static FlowDocument Load(string path)
        {
            using FileStream fs = File.Open(path, FileMode.Open);

            FlowDocument? document = XamlReader.Load(fs) as FlowDocument;
            return document ?? throw new FileNotFoundException();
        }
    }
}
