using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace MyNotepad.ViewModel
{
    public class PasswordValidationRule : ValidationRule
    {
        Regex regex = new Regex(@"[a-zA-Z0-9-_\.]{1,10}$");

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string password = value.ToString();
            if (!regex.IsMatch(password)) return new ValidationResult(false, "ограничение от 2 до 10 символов, которыми могут быть буквы и цифры");
            return new ValidationResult(true, null);
        }
    }
}
