using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notepade2
{
    internal class Recording
    {
        private string firstName;
        public string FirstName 
        {
            get {return firstName; } 
            set { firstName = value.Length>=1 ? value : "Безымянный"; } 
        }


        private string lastName;
        public string LastName
        {
            get { return lastName; }
            set { lastName = value.Length >= 1 ? value : "Без фамилии"; }
        }


        public ContactData ContactData { get; set; }



        private string note;
        public string Note
        {
            get { return note; }
            set { note = value.Length >= 1 ? value : "Заметка не указана"; }
        }
    }
}
