using System.IO;
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
            Project? project = XamlReader.Load(fs) as Project;
            return project ?? throw new FileNotFoundException();
        }
    }
}
