using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Notepade2
{
    internal class ContactData
    {
        private string phone;
        public string Phone

        {
            get { return phone; }
            set

            {
                phone = Regex.Replace(value, @"^8", "+7").Replace(@"\D", "");

            }
        }
        private string adress;
        public string Adress
        {
            get { return adress; }
            set { adress = value.Length >= 1 ? value : "Адрес не указан"; }
        }
    }
}
