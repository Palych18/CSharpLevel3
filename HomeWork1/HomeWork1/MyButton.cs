using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls.Primitives;

namespace HomeWork1
{
    class MyButton : ButtonBase
    {
        public static readonly DependencyProperty HomeWork1Property;

        static MyButton()
        {

            MyButton.HomeWork1Property = DependencyProperty.Register("HomeWork1",
                typeof(bool), //тип
                typeof(MyButton), //класс
                new FrameworkPropertyMetadata(false, new PropertyChangedCallback(HomeWork1PropertyChanged)));
        }

        private static void HomeWork1PropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
           
        }

        public bool HomeWork1
        {
            get { return (bool)GetValue(MyButton.HomeWork1Property); }
            set { SetValue(MyButton.HomeWork1Property, value); }
        }
    }
}
