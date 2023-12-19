using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notepade2
{
    internal class User

    {
        public List<Recording> records;

        public User()
        {
            records = new List<Recording>();
        }

        public void AddRecording(Recording recording)
        {
            records.Add(recording);
        }

        public void DeleteRecording(Recording recording)
        {
            records.Remove(recording);
        }

        public void EditRecord(Recording oldRecord, Recording newRecord)
        {
            int index = records.IndexOf(oldRecord);
            if (index != -1)
            {
                records[index] = newRecord;
            }
        }

        public Recording GetNameData(string searchFirstOrLastName)
        {
            // Читерский код
            //return records.FirstOrDefault(r => r.FirstName.Contains(searchFirstOrLastName) || r.LastName.Contains(searchFirstOrLastName));

            foreach (var recording in records)
            {
                if (recording.FirstName.ToLower().Contains(searchFirstOrLastName) ||
                    recording.LastName.ToLower().Contains(searchFirstOrLastName) ||
                    recording.ContactData.Phone.ToLower().Contains(searchFirstOrLastName) ||
                    recording.ContactData.Adress.ToLower().Contains(searchFirstOrLastName) ||
                    recording.Note.ToLower().Contains(searchFirstOrLastName))
                {
                    return recording;
                }
            }
            return null;
        }

        public void PrintRecord(Recording recording)
        {
            Console.WriteLine(new string('-', 60));
            Console.WriteLine($"\tИмя: {recording.FirstName}");
            Console.WriteLine($"\tФамилия: {recording.LastName}");
            Console.WriteLine($"\tТелефон: {recording.ContactData.Phone}");
            Console.WriteLine($"\tАдрес: {recording.ContactData.Adress}");
            Console.WriteLine($"\tПримечание: {recording.Note}");
            
        }

    }
}
