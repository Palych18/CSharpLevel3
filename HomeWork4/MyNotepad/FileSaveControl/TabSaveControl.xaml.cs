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

namespace FileSaveControl
{
    /// <summary>
    /// Логика взаимодействия для UserControl1.xaml
    /// </summary>
    public partial class TabSaveControl : UserControl
    {
        public TabSaveControl()
        {
            InitializeComponent();
            btnSave.Click += btnSave_Click;
            }

        public static readonly DependencyProperty SaveTextProperty = DependencyProperty.Register("SaveText", typeof(string), typeof(TabSaveControl), new PropertyMetadata("Save"));
                
        public string SaveText
        {
            get
            {
                return (string)GetValue(SaveTextProperty);
            }
            set
            {
                SetValue(SaveTextProperty, value);
            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            e.Handled = true;
            RoutedEventArgs args = new RoutedEventArgs(btnSaveClickEvent);
            RaiseEvent(args);
        }
        
        public static readonly RoutedEvent btnSaveClickEvent = EventManager.RegisterRoutedEvent("btnSaveClick", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(TabSaveControl));
        
        public event RoutedEventHandler btnSaveClick
        {
            add { AddHandler(btnSaveClickEvent, value); }
            remove { RemoveHandler(btnSaveClickEvent, value); }
        }
    }
}
