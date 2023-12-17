using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notepade2
{
    internal class Recording
    {
        private string identificationData;
        public string IdentificationData 
        {
            get {return identificationData;} 
            set { identificationData = value==null ? value : "Безымянный"; } 
        }
        
        public ContactData ContactData { get; set; }


        private string note;
        public string Note
        {
            get { return note; }
            set { note = value == null ? value : "Заметка не указана"; }
        }
    }
}
