using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notepade2
{
    internal class ContactData
    {
        public string Phone { get; set; }
        private string adress;
        public string Adress
        {
            get { return adress; }
            set { adress = value == null ? value : "Адрес не указан"; }
        }
    }
}
