using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Text.RegularExpressions;
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
                        var newRecording = AssignValues(firstName, lastName, phone, adress, note);
                        currentUser.AddRecording(newRecording);
                    }
                }
                if (currentUser != null)
                {
                    users.Add(currentUser);  // Добавляем последнего пользователя в список пользователей
                }
            }
        }
             

        public void AddNewRecord()
        {
            User user = new User();
            Utils.SetHeadlineOrEnding("Введите данные для новой записи:");

            var firstName = Utils.RequestData("Введите имя:  ");

            var lastName = Utils.RequestData("Введите фамилию:  ");

            var phone = Utils.RequestData("Введите телефон:  ");
            Utils.CheckRegex(phone);

            string adress = Utils.RequestData("Введите адрес:  ");

            string note = Utils.RequestData("Введите текст заметки:  ");
            Utils.CheckNote(note);

            var newRecording = AssignValues(firstName, lastName, phone, adress, note);
            user.AddRecording(newRecording);
            AddUser(user);
            SaveTextFile();

            Utils.SetHeadlineOrEnding("Запись добавлена");
            Utils.PressKeyToContinue();
        }

        public void DeleteRecord()
        {
            string nameToDelete = Utils.RequestData("Введите Имя или Фамилию контакта для удаления:  ").ToLower();
            bool isRecordDeleted = false;
            foreach (User userDel in Users)
            {
                Recording recording = userDel.GetNameData(nameToDelete);
                if (recording != null)
                {
                    foreach (Recording record in userDel.records)
                    {
                        userDel.PrintRecord(record);
                    }
                    var confirmation = Utils.RequestYesOrNo("Вы уверены, что хотите удалить эту запись? (да/нет):  ");
                    if (confirmation.ToLower() == "да")
                    {
                        userDel.DeleteRecording(recording);
                        Utils.SetHeadlineOrEnding("Запись удалена");
                        isRecordDeleted = true;
                        break;
                    }
                    else
                    {
                        Utils.SetHeadlineOrEnding("Удаление отменено");
                        isRecordDeleted = true;
                        break;
                    }
                }
            }
            if (!isRecordDeleted)
            {
                Utils.SetHeadlineOrEnding("Запись с указанными данными не найдена");
            }
            SaveTextFile();
            Utils.PressKeyToContinue();
        }
        public void EditRecord()
        {
            var nameToEdit = Utils.RequestData("Введите Имя или Фамилию контакта для редактирования:  ").ToLower();
            bool isRecordEdit = false;
            foreach (User userEdit in Users)
            {
                Recording recording = userEdit.GetNameData(nameToEdit);
                if (recording != null)
                {

                    foreach (Recording record in userEdit.records)
                    {
                        userEdit.PrintRecord(record);
                    }
                    var confirmation = Utils.RequestYesOrNo("Вы уверены, что хотите отредактировать эту запись? (да/нет):  ");
                    if (confirmation.ToLower() == "да")
                    {
                        var inputFirstName = Utils.RequestData("Введите имя: ");
                        string newFirstName = string.IsNullOrEmpty(inputFirstName) ? recording.FirstName : inputFirstName;

                        var inputLastName = Utils.RequestData("Введите фамилию: ");
                        string newLastName = string.IsNullOrEmpty(inputLastName) ? recording.LastName : inputLastName;

                        var inputPhone = Utils.RequestData("Введите телефонный номер: ");
                        string newPhone = string.IsNullOrEmpty(inputPhone) ? recording.ContactData.Phone : inputPhone;
                        Utils.CheckRegex(newPhone);

                        var inputAddress = Utils.RequestData("Введите адрес: ");
                        string newAdress = string.IsNullOrEmpty(inputAddress) ? recording.ContactData.Adress : inputAddress;

                        var inputNote = Utils.RequestData("Введите текст заметки: ");
                        string newNote = string.IsNullOrEmpty(inputNote) ? recording.Note : inputNote;
                        Utils.CheckNote(newNote);

                        var newEditRecording = AssignValues(newFirstName, newLastName, newPhone, newAdress, newNote);

                        userEdit.EditRecord(recording, newEditRecording);
                        Utils.SetHeadlineOrEnding("Запись отредактирована");
                        isRecordEdit = true;
                        break;
                    }
                    else
                    {
                        Utils.SetHeadlineOrEnding("Редактирование отменено");
                        isRecordEdit = true;
                        break;
                    }
                }
            }
            if (!isRecordEdit)
            {
                Utils.SetHeadlineOrEnding("Контакт  не найден");
            }
            SaveTextFile();
            Utils.PressKeyToContinue();
        }
        public void SortRecord()
        {
            var choiceSorting = Utils.RequestYesOrNo("Выберите сортировку: \n\t 1. Отсортировать по имени\n\t 2. Отсортировать по фамилии");
            switch (choiceSorting)
            {
                case "1":
                    Utils.SetHeadlineOrEnding("Сортировка по имени");
                    AlphabeticalSortingFirstName();
                    break;
                case "2":
                    Utils.SetHeadlineOrEnding("Сортировка по фамилии");
                    AlphabeticalSortingLastName();
                    break;
                default:
                    Utils.SetHeadlineOrEnding("Некорректный выбор. Попробуйте еще раз.");
                    break;

            }
            Utils.PressKeyToContinue();
        }

        public void SearchRecord()
        {
            var nameToSearch = Utils.RequestData("Введите Имя или Фамилию контакта для поиска:  "); 
            bool isRecordSearch = false;
            Utils.SetHeadlineOrEnding("Результат поиска:" + nameToSearch);
            foreach (User userEdit in Users)
            {
                Recording recording = userEdit.GetNameData(nameToSearch);
                if (recording != null)
                {

                    foreach (Recording record in userEdit.records)
                    {
                        userEdit.PrintRecord(record);
                    }
                    isRecordSearch = true;
                }
            }
            if (!isRecordSearch)
            {
                Utils.SetHeadlineOrEnding("Контакт не найден. Попробуйте ввести другие данные для поиска");
            }
            Utils.PressKeyToContinue();
        }

        public void AlphabeticalSortingFirstName()
        {
            users = users.OrderBy(u => u.records.First().FirstName).ToList();
            GetAllRecordings();
        }

        public void AlphabeticalSortingLastName()
        {
            users = users.OrderBy(u => u.records.First().LastName).ToList();

            GetAllRecordings();
        }

        public Recording AssignValues(string firstName, string lastName, string phone, string adress, string note)
        {

            ContactData contactData = new ContactData()
            {
                Phone = phone,
                Adress = adress
            };

            Recording newRecording = new Recording()
            {
                FirstName = firstName,
                LastName = lastName,
                ContactData = contactData,
                Note = note
            };
            return newRecording;

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

    }
}



