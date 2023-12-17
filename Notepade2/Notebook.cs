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

        public List<User> Users => users;

        public void AddUser(User user)
        {
            users.Add(user);
        }

        public void SaveTextFile()
        {
            using (StreamWriter writer = new StreamWriter("Notebook.txt"))
            {
                foreach (User user in users)
                {
                    foreach (Recording recording in user.records)
                    {
                        writer.WriteLine($"Имя: {recording.FirstName}");
                        writer.WriteLine($"Фамилия: {recording.LastName}");
                        writer.WriteLine($"Телефон: {recording.ContactData.Phone}");
                        writer.WriteLine($"Адрес: {recording.ContactData.Adress}");
                        writer.WriteLine($"Примечание: {recording.Note}");

                    }
                }
            }
        }

        public void LoadFromTextFile()
        {
            using (StreamReader reader = new StreamReader("Notebook.txt"))
            {
                string line;
                User currentUser = null;
                while ((line = reader.ReadLine()) != null)
                {
                    if (line.StartsWith("Имя: "))
                    {
                        if (currentUser != null)
                        {
                            users.Add(currentUser);  // Добавляем текущего пользователя в список пользователей перед переходом к новому пользователю

                        }
                        currentUser = new User();
                        string firstName = line.Substring("Имя: ".Length);
                        string lastName = reader.ReadLine().Substring("Фамилия: ".Length);
                        string phone = reader.ReadLine().Substring("Телефон: ".Length);
                        string adress = reader.ReadLine().Substring("Адрес: ".Length);
                        string note = reader.ReadLine().Substring("Примечание: ".Length);


                        Recording newRecording = new Recording()
                        {
                            FirstName = firstName,
                            LastName = lastName,
                            ContactData = new ContactData
                            {
                                Phone = phone,
                                Adress = adress
                            },
                            Note = note
                        };

                        currentUser.AddRecording(newRecording);
                    }
                }
                if (currentUser != null)
                {
                    users.Add(currentUser);  // Добавляем последнего пользователя в список пользователей
                }
            }
        }

        public void GetAllRecordings()
        {
            foreach (User user in users)
            {
                foreach (Recording recording in user.records)
                {
                    Console.WriteLine(new string('-', 60));
                    Console.WriteLine($"Имя: {recording.FirstName}");
                    Console.WriteLine($"Фамилия: {recording.LastName}");
                    Console.WriteLine($"Телефон: {recording.ContactData.Phone}");
                    Console.WriteLine($"Адрес: {recording.ContactData.Adress}");
                    Console.WriteLine($"Примечание: {recording.Note}");

                }
            }
        }


        public void AlphabeticalSorting()
        {   // Аналогичный читерский код
            //users = users.OrderBy(u => u.records.First().FirstName).ToList();

            User[] usersArray = users.ToArray();

            for (int i = 0; i < usersArray.Length - 1; i++)
            {
                for (int j = 0; j < usersArray.Length - 1 - i; j++)
                {
                    string firstName1 = usersArray[j].records.First().FirstName;
                    string firstName2 = usersArray[j + 1].records.First().FirstName;

                    if (firstName1.CompareTo(firstName2) > 0)
                    {
                        User temp = usersArray[j];
                        usersArray[j] = usersArray[j + 1];
                        usersArray[j + 1] = temp;
                    }
                }
            }
            users.Clear();
            users.AddRange(usersArray);

            GetAllRecordings();
        }
        

    }

}



