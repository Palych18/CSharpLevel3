using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace MyNotepad.ViewModel
{
    public class ViewModel : INotifyPropertyChanged
    {                
        public event PropertyChangedEventHandler PropertyChanged;

        bool access;
        int attempCount = 0;

        public int AttemptCount
        {
            get
            {
                return attempCount;
            }
            set
            {
                attempCount = value;
                PropertyChanged?.Invoke(this,new PropertyChangedEventArgs("AttemptCount"));
            }
        }

        public Model.Account Account { get; set; } = new Model.Account("", "");
        
        Model.Accounts accounts = new Model.Accounts();

        private void Execute(object obj)
        {
            Access = accounts.Checks(Account);
            AttemptCount++;
        }

        public ICommand ClickAccess
        {
            get
            {
                return new DelegateCommand(Execute,CanExecute);
            }
        }

        private bool CanExecute(object obj)
        {
            return AttemptCount < 3 && Access==false;
        }

        public bool Access
        {
            get
            {
                return access;
            }
            set
            {
                if (access != value)
                {
                    access = value;
                    Console.WriteLine(access);
                    PropertyChanged.Invoke(this, new PropertyChangedEventArgs("Access"));
                }
            }
        }
    }
}
