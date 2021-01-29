using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNotepad.Model
{
    public class Account : INotifyPropertyChanged
    {
        private string login = "None";
        private string password = "None";

        public event PropertyChangedEventHandler PropertyChanged;

        public string Login
        {
            get => login; set
            {
                if (value != login)
                {
                    login = value;        
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Login"));
                }
            }
        }
        public string Password 
        {
            get => password; set
            {
                if (value != password)
                {
                    password = value;          
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Password"));
                }
            }
        }
        public Account(string login, string password)
        {
            Login = login;
            Password = password;
        }
    }

    class Accounts
    {
        public bool Checks(Account account)
        {
            foreach (Account acc in ListAccounts)
                if (acc.Login == account.Login && acc.Password == account.Password) return true;
            return false;
        }
        
         public static IEnumerable<Account> ListAccounts
        {
            get;
        }
            = new List<Account>() {
                                  new Account("root", "root"),
                                  new Account("login","password"),
                                  new Account("admin","admin")
                                  };
    }
}
