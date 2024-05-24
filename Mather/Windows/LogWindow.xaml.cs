using Mather.Data.States;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Mather
{
    /// <summary>
    /// Логика взаимодействия для LogWindow.xaml
    /// </summary>
    public partial class LogWindow : Window
    {
        FlowDocument document;
        public LogWindow()
        {
            InitializeComponent();
            var dialog = new OpenFileDialog();
            var isGetPath = dialog.ShowDialog();
            if (isGetPath != null && isGetPath == true)
            {
                document = XamlManager.Load<FlowDocument>(dialog.FileName);
                DocumentViever.Document = document;
            }
            else
            {
                this.Close();
            }
        }

        public LogWindow(FlowDocument flowDocument)
        {
            InitializeComponent();
            document = flowDocument;
            DocumentViever.Document = document;
        }
    }
}
