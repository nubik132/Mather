﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Markup;

namespace Mather.Data.States
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

        public static void SaveProject(Project project, string path)
        {
            using FileStream fs = File.Open(path, FileMode.Create);

            XamlWriter.Save(project, fs);
        }

        public static Project LoadProject(string path)
        {
            using FileStream fs = File.Open(path, FileMode.Open);

            Project? document = XamlReader.Load(fs) as Project;
            return document ?? throw new FileNotFoundException();
        }
    }
}