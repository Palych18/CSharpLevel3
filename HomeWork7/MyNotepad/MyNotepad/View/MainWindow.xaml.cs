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
using System.Data.Entity;

namespace MyNotepad
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>    

    public partial class MainWindow : Window
    {
        MyNotepad.DatabaseContainer _dbContainer;
        public List<MailAdress> MailAdressList;

        public MainWindow()
        {
            InitializeComponent();           
        }

        private void MainWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            _dbContainer = new DatabaseContainer();
            _dbContainer.MailAdressSet.Load();
            dgGrid.ItemsSource = _dbContainer.MailAdressSet.Local;
        }

        private void btnAccess_OnClick(object sender, RoutedEventArgs e)
        {
            _dbContainer.MailAdressSet.Add(new MailAdress
            {
                Sender = tbSender.Text,
                Addressee = tbAdressee.Text,
                Message = tbMessage.Text
            });

            _dbContainer.SaveChanges();
        }

        private void TabSwitcher_btnBackClick(object sender, RoutedEventArgs e)
        {
            if (tabControl.SelectedIndex == 0) tabControl.SelectedIndex = tabControl.Items.Count - 1; else tabControl.SelectedIndex--;
        }

        private void TabSwitcher_btnNextClick(object sender, RoutedEventArgs e)
        {
            if (tabControl.SelectedIndex == tabControl.Items.Count - 1) tabControl.SelectedIndex -= tabControl.SelectedIndex; else tabControl.SelectedIndex++;
        }

        private void btnClearMessage_Click(object sender, RoutedEventArgs e)
        {
            tbMessage.Clear();
            tbMessage.Text = string.Empty;
        }

        private void btnClearSender_Click(object sender, RoutedEventArgs e)
        {
            tbSender.Clear();
            tbSender.Text = string.Empty;
        }

        private void btnClearAdressee_Click(object sender, RoutedEventArgs e)
        {
            tbAdressee.Clear();
            tbAdressee.Text = string.Empty;
        }

        private void btnPasteDate_Click(object sender, RoutedEventArgs e)
        {
            rtbNotepad.AppendText(DateTime.Now.ToLongDateString() + "\n");
        }

        private void btnPasteTime_Click(object sender, RoutedEventArgs e)
        {
            rtbNotepad.AppendText(DateTime.Now.ToLongTimeString() + "\n");
        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            rtbNotepad.Document.Blocks.Clear();
            rtbNotepad.Document.Blocks.Add(new Paragraph(new Run("")));
        }

        private void btnClearJournal_Click(object sender, RoutedEventArgs e)
        {
            dgGrid.Items.Clear();            
        }

        private void btnPaste_Click(object sender, RoutedEventArgs e)
        {
           dgGrid.ItemsSource = MailAdressList;
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            for (var vis = sender as Visual; vis != null; vis = VisualTreeHelper.GetParent(vis) as Visual)
            {
                if(vis is DataGrid)
                {
                    var row = (DataGridRow)vis;
                    int id = (row.Item as MailAdress).Id;

                    MailAdress mailAdress = _dbContainer.MailAdressSet
                        .Where(o => o.Id == id)
                        .FirstOrDefault();
                    if (mailAdress != null)
                    {
                        _dbContainer.MailAdressSet.Remove(mailAdress);
                        _dbContainer.SaveChanges();                        
                    }
                    break;
                }
            }
        }
    }
}
