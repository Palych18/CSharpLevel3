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

namespace FileTabControl
{
    /// <summary>
    /// Логика взаимодействия для TabSwitcher.xaml
    /// </summary>
    public partial class TabSwitcher
    {
        public TabSwitcher()
        {
            InitializeComponent();
            btnBack.Click += btnBack_Click;
            btnNext.Click += BtnNext_Click;
        }

        public static readonly DependencyProperty BackTextProperty = DependencyProperty.Register("BackText", typeof(string), typeof(TabSwitcher), new PropertyMetadata("Back"));
       
        public static readonly DependencyProperty NextTextProperty = DependencyProperty.Register("NextText", typeof(string), typeof(TabSwitcher), new PropertyMetadata("Next"));

        public string BackText
        {
            get
            {
                return (string)GetValue(BackTextProperty);
            }
            set
            {
                SetValue(BackTextProperty, value);
            }
        }

        public string NextText
        {
            get
            {
                return (string)GetValue(NextTextProperty);
            }
            set
            {
                SetValue(NextTextProperty, value);
            }
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            e.Handled = true;
            RoutedEventArgs args = new RoutedEventArgs(btnBackClickEvent);
            RaiseEvent(args);
        }

        private void BtnNext_Click(object sender, RoutedEventArgs e)
        {
            e.Handled = true;
            RoutedEventArgs args = new RoutedEventArgs(btnNextClickEvent);
            RaiseEvent(args);
        }

        public static readonly RoutedEvent btnBackClickEvent = EventManager.RegisterRoutedEvent("btnBackClick", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(TabSwitcher));
        public static readonly RoutedEvent btnNextClickEvent = EventManager.RegisterRoutedEvent("btnNextClick", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(TabSwitcher));

        public event RoutedEventHandler btnBackClick
        {
            add { AddHandler(btnBackClickEvent, value); }
            remove { RemoveHandler(btnBackClickEvent, value); }
        }


        public event RoutedEventHandler btnNextClick
        {
            add { AddHandler(btnNextClickEvent, value); }
            remove { RemoveHandler(btnNextClickEvent, value); }
        }
    }
}
