using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notepade2
{
    internal class Notebook
    {
        private List<User> users;

        public Notebook()
        {
            users = new List<User>();
        }

        public void AddUser(User user)
        {
            users.Add(user);
        }

        public void SaveTextFile(string path)
        {
            using (StreamWriter writer = new StreamWriter(path))
            {
                foreach (User user in users)
                {
                    foreach (Recording recording in user.records)
                    {
                        writer.WriteLine($"Идентификационные данные: {recording.IdentificationData}");
                        writer.WriteLine($"Телефон: {recording.ContactData.Phone}");
                        writer.WriteLine($"Адрес: {recording.ContactData.Adress}");
                        writer.WriteLine($"Примечание: {recording.Note}");
                        writer.WriteLine();
                    }
                }
            }
        }

        public void LoadFromTextFile(string path)
        {
            using (StreamReader reader = new StreamReader(path))
            {
                string line;
                string identificationData = "";
                string phone = "";
                string adress = "";
                string note = "";

                while ((line = reader.ReadLine()) != null)
                {
                    switch (line)
                    {
                        case string s when s.StartsWith("Идентификационные данные: "):
                            identificationData = line.Substring("Идентификационные данные: ".Length);
                            break;
                        case string s when s.StartsWith("Телефон: "):
                            phone = line.Substring("Телефон: ".Length);
                            break;
                        case string s when s.StartsWith("Адрес: "):
                            adress = line.Substring("Адрес: ".Length);
                            break;
                        case string s when s.StartsWith("Примечание: "):
                            note = line.Substring("Примечание: ".Length);
                            break;
                        default:
                            break;
                    }
                    Recording newRecord = new Recording
                    {
                        IdentificationData = identificationData,
                        ContactData = new ContactData
                        {
                            Phone = phone,
                            Adress = adress
                        },
                        Note = note
                    };
                    // Добавить новую запись в активного абонента
                }
            }
        }
    }

}

