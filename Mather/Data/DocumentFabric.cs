using System.Windows.Documents;

namespace Mather.Data
{
    public static class DocumentFabric
    {
        public static FlowDocument Custom(string text)
        {
            return new FlowDocument(new Paragraph(new Run(text)));
        }
        public static FlowDocument Default()
        {
            return Custom("Новый документ");
        }

        public static FlowDocument DefaultTask()
        {
            return Custom("Новое задание");
        }
    }
}
