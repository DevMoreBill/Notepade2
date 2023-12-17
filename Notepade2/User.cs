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

        public Recording GetRecordingByIdentificationData(string identificationData)
        {
            return records.FirstOrDefault(r => r.IdentificationData.Contains(identificationData));
        }
    }
}
