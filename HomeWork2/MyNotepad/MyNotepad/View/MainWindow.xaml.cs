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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MyNotepad
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            rtbNotepad.Document.Blocks.Clear();
            rtbNotepad.Document.Blocks.Add(new Paragraph(new Run("")));
        }

        private void btnPasteDate_Click(object sender, RoutedEventArgs e)
        {
            rtbNotepad.AppendText(DateTime.Now.ToLongDateString() + "\n");
        }

        private void btnPasteTime_Click(object sender, RoutedEventArgs e)
        {
            rtbNotepad.AppendText(DateTime.Now.ToLongTimeString() + "\n");
        }

        private void TabSwitcher_btnBackClick(object sender, RoutedEventArgs e)
        {
            if (tabControl.SelectedIndex == 0) tabControl.SelectedIndex = tabControl.Items.Count - 1; else tabControl.SelectedIndex--;
        }

        private void TabSwitcher_btnNextClick(object sender, RoutedEventArgs e)
        {
            if (tabControl.SelectedIndex == tabControl.Items.Count - 1) tabControl.SelectedIndex -= tabControl.SelectedIndex; else tabControl.SelectedIndex++;
        }
    }
}
