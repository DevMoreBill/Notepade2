using System;
using System.Numerics;
using System.Text.RegularExpressions;

namespace Notepade2
{
    class Program
    {
        static void Main(string[] args)
        {
            Notebook notebook = new Notebook();

            notebook.LoadFromTextFile();
            //Многовариантная регулярка для номера телефона честно сперто
            //+7-916-777-88-00
            //+7-916-777-8800
            //+47-916-777-88-00
            //+47-916-7778800
            //+47(916)777-88-00
            //+47(916)777-8800
            //+47(916)7778800
            //(916)777-88-00
            //(916)777-8800
            //(916)777-8-800
            //(916)7778800
            //9167778800
            //916-7778800
            //916-777-88-00
            //916-777-8-800
            string phoneNumberCheck = @"(^\+\d{1,2})?((\(\d{3}\))|(\-?\d{3}\-)|(\d{3}))((\d{3}\-\d{4})|(\d{3}\-\d\d\-\d\d)|(\d{7})|(\d{3}\-\d\-\d{3}))";
            //Это был мой вариант регулярки для номера телефона
            //string phoneNumberCheck = @"\+7-[0-9]{3}-[0-9]{3}-[0-9]{2}-[0-9]{2}";
            while (true)
            {
                Console.WriteLine(new string('-', 60));
                Console.WriteLine("Выберите действие:");
                Console.WriteLine("\t1. Добавить запись");
                Console.WriteLine("\t2. Удалить запись");
                Console.WriteLine("\t3. Редактировать запись");
                Console.WriteLine("\t4. Вывести все записи в алфавитном порядке");
                Console.WriteLine("\t5. Выход");
                Console.WriteLine(new string('-', 60));

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        User user = new User();
                        Console.WriteLine(new string('-', 60));
                        Console.WriteLine("Введите данные для новой записи:");
                        Console.WriteLine(new string('-', 60));

                        Console.Write("Введите имя: ");
                        string firstName = Console.ReadLine();
                        Console.Write("Введите фамилию: ");
                        string lastName = Console.ReadLine();
                        string phone;
                        do
                        {
                            Console.Write("Введите телефонный номер: ");
                            phone = Console.ReadLine();
                            if (!Regex.IsMatch(phone, phoneNumberCheck))
                            {
                                Console.WriteLine("Некорректный формат номера телефона. Попробуйте еще раз.");
                            }
                        } while (!Regex.IsMatch(phone, phoneNumberCheck));

                        Console.Write("Введите адрес: ");
                        string adress = Console.ReadLine();

                        Console.Write("Введите текст заметки: ");
                        string note = Console.ReadLine();
                        if (note.Length > 100)
                        {
                            Console.WriteLine("Заметка более 100 символов, будут добавлены только первые 100 символов.");
                            note = note.Remove(100);
                        }

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

                        user.AddRecording(newRecording);
                        notebook.AddUser(user);
                        notebook.SaveTextFile();
                        Console.WriteLine("Запись добавлена");
                        Console.WriteLine(new string('-', 60));
                        break;


                    case "2":
                        Console.WriteLine(new string('-', 60));
                        Console.WriteLine("Введите Имя или Фамилию контакта для удаления:");
                        Console.WriteLine(new string('-', 60));
                        string nameToDelete = Console.ReadLine();
                        bool isRecordDeleted = false;

                        foreach (User userDel in notebook.Users)
                        {
                            Recording recording = userDel.GetNameData(nameToDelete);
                            if (recording != null)
                            {
                                Console.WriteLine("Вы уверены, что хотите удалить эту запись? (да/нет)");
                                foreach (Recording record in userDel.records)
                                {
                                    userDel.PrintRecord(record);

                                }
                                string confirmation = Console.ReadLine();
                                if (confirmation.ToLower() == "да")
                                {
                                    userDel.DeleteRecording(recording);
                                    Console.WriteLine("Запись удалена");
                                    Console.WriteLine(new string('-', 60));
                                    isRecordDeleted = true;
                                    break;
                                }
                                else
                                {
                                    Console.WriteLine("Удаление отменено");
                                    Console.WriteLine(new string('-', 60));
                                    isRecordDeleted = true;
                                    break;
                                }
                            }
                        }
                        if (!isRecordDeleted)
                        {
                            Console.WriteLine("Запись с указанными данными не найдена");
                            Console.WriteLine(new string('-', 60));
                        }
                        notebook.SaveTextFile();
                        break;
                    case "3":
                        Console.WriteLine(new string('-', 60));
                        Console.WriteLine("Введите Имя или Фамилию контакта для редактирования:");
                        Console.WriteLine(new string('-', 60));
                        string nameToEdit = Console.ReadLine();
                        bool isRecordEdit = false;
                        // Поиск записи по идентификационным данным и редактирование

                        foreach (User userEdit in notebook.Users)
                        {
                            Recording recording = userEdit.GetNameData(nameToEdit);
                            if (recording != null)
                            {
                                Console.WriteLine("Вы хотите отредактировать этот контакт? (да/нет)");
                                foreach (Recording record in userEdit.records)
                                {
                                    userEdit.PrintRecord(record);
                                }

                                string confirmation = Console.ReadLine();
                                if (confirmation.ToLower() == "да")
                                {
                                    //Добавляем новое имя, если поле пустое, оставляеем прежнее значение
                                    Console.Write("Введите Имя: ");
                                    string inputFirstName = Console.ReadLine();
                                    string newFirstName = string.IsNullOrEmpty(inputFirstName) ? recording.FirstName : inputFirstName;

                                    //Добавляем новую фамилию, если поле пустое, оставляеем прежнее значение
                                    Console.Write("Введите фамилию: ");
                                    string inputLastName = Console.ReadLine();
                                    string newLastName = string.IsNullOrEmpty(inputLastName) ? recording.LastName : inputLastName;

                                    string newPhone;
                                    do
                                    {
                                        Console.Write("Введите телефонный номер: ");
                                        newPhone = Console.ReadLine();
                                        if (!Regex.IsMatch(newPhone, phoneNumberCheck))
                                        {
                                            Console.WriteLine("Некорректный формат номера телефона. Попробуйте еще раз.");
                                        }
                                    } while (!Regex.IsMatch(newPhone, phoneNumberCheck));

                                    //Добавляем новый адрес, если поле пустое, оставляеем прежнее значение
                                    Console.Write("Введите адрес: ");
                                    string inputAddress = Console.ReadLine();
                                    string newAdress = string.IsNullOrEmpty(inputAddress) ? recording.ContactData.Adress : inputAddress;

                                    //Добавляем новую заметку, если поле пустое, оставляеем прежнее значение
                                    Console.Write("Введите текст заметки: ");
                                    string inputNote = Console.ReadLine();
                                    string newNote = string.IsNullOrEmpty(inputNote) ? recording.Note : inputNote;
                                    if (newNote.Length > 100)
                                    {
                                        Console.WriteLine("Заметка более 100 символов, будут добавлены только первые 100 символов.");
                                        newNote = newNote.Remove(100);
                                    }


                                    ContactData newContactData = new ContactData()
                                    {
                                        Phone = newPhone,
                                        Adress = newAdress
                                    };
                                    Recording newEditRecording = new Recording

                                    {
                                        FirstName = newFirstName,
                                        LastName = newLastName,
                                        ContactData = newContactData,
                                        Note = newNote

                                    };


                                    userEdit.EditRecord(recording, newEditRecording);
                                    Console.WriteLine("Запись отредактирована");
                                    isRecordEdit = true;
                                    break;
                                }
                                else
                                {
                                    Console.WriteLine(new string('-', 60));
                                    Console.WriteLine("Изменение контакта отменено");
                                    isRecordEdit = true;
                                    break;
                                }
                            }
                        }
                        if (!isRecordEdit)
                        {
                            Console.WriteLine(new string('-', 60));
                            Console.WriteLine("Контакт  не найдена");

                        }
                        notebook.SaveTextFile();
                        break;
                    case "4":
                        Console.WriteLine(new string('-', 60));
                        Console.WriteLine("Выберите сортировку:");
                        Console.WriteLine("\t 1. Отсортировать по имени");
                        Console.WriteLine("\t 2. Отсортировать по фамилии");
                        Console.WriteLine(new string('-', 60));
                        string choiceSorting = Console.ReadLine();
                        switch (choiceSorting)
                        {
                            case "1":
                                Console.WriteLine(new string('-', 60));
                                Console.WriteLine(" Выбрана сортировка по имени");
                                notebook.AlphabeticalSortingFirstName();
                                break;
                            case "2":
                                Console.WriteLine(new string('-', 60));
                                Console.WriteLine(" Выбрана сортировка по фамилии");
                                notebook.AlphabeticalSortingLastName();
                                break;
                            default:
                                Console.WriteLine(new string('-', 60));
                                Console.WriteLine("Некорректный выбор. Попробуйте еще раз.");
                                break;

                        }
                        break;

                    case "5":
                        Environment.Exit(0);
                        return;
                    default:
                        Console.WriteLine("Некорректный выбор. Попробуйте еще раз.");
                        break;
                }
            }
        }
    }
}
