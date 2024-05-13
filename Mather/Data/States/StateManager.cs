using System.IO;
using System.Windows.Markup;
using System.Xml.Serialization;

namespace Mather.Data.States
{
    public static class StateManager
    {
        public static void Save<T>(T obj, string path)
        {
            using FileStream fs = File.Open(path, FileMode.Create);
            XamlWriter.Save(obj, fs);
        }

        public static T Load<T>(string path)
        {
            using FileStream fs = File.Open(path, FileMode.Open);
            T? obj = (T)XamlReader.Load(fs);
            return obj ?? throw new FileNotFoundException();
        }
    }
}
