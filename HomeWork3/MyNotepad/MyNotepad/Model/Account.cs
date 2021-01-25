using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNotepad.Model
{
    public class Account
    {
        public string Login { get; set; } = "None";
        public string Password { get; set; } = "None";
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
