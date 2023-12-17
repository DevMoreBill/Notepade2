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
            //string phoneNumberCheck = @"\+7-[0-9]{3}-[0-9]{3}-[0-9]{2}-[0-9]{2}";
            while (true)
            {
                Console.WriteLine(new string('-', 50));
                Console.WriteLine("Выберите действие:");
                Console.WriteLine("\t1. Добавить запись");
                Console.WriteLine("\t2. Удалить запись");
                Console.WriteLine("\t3. Редактировать запись");
                Console.WriteLine("\t4. Вывести все записи");
                Console.WriteLine("\t5. Выход");
                Console.WriteLine(new string('-', 50));

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        User user = new User();
                        Console.WriteLine(new string('-', 50));
                        Console.WriteLine("Введите данные для новой записи:");
                        Console.WriteLine(new string('-', 50));

                        Console.Write("Введите ФИО: ");
                        string identificationData = Console.ReadLine();
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
                            IdentificationData = identificationData,
                            ContactData = contactData,
                            Note = note
                        };

                        user.AddRecording(newRecording);
                        notebook.AddUser(user);
                        notebook.SaveTextFile();
                        Console.WriteLine("Запись добавлена");
                        Console.WriteLine(new string('-', 50));
                        break;


                    case "2":
                        Console.WriteLine("Введите данные записи для удаления:");
                        string identificationDataToDelete = Console.ReadLine();
                        bool isRecordDeleted = false;

                        foreach (User userDel in notebook.Users)
                        {
                            Recording recording = userDel.GetRecordingByIdentificationData(identificationDataToDelete);
                            if (recording != null)
                            {
                                Console.WriteLine("Вы уверены, что хотите удалить эту запись? (да/нет)");
                                string confirmation = Console.ReadLine();
                                if (confirmation.ToLower() == "да")
                                {
                                    userDel.DeleteRecording(recording);
                                    Console.WriteLine("Запись удалена");
                                    Console.WriteLine(new string('-', 50));
                                    isRecordDeleted = true;
                                    break;
                                }
                                else
                                {
                                    Console.WriteLine("Удаление отменено");
                                    Console.WriteLine(new string('-', 50));
                                    isRecordDeleted = true;
                                    break;
                                }
                            }
                        }
                        if (!isRecordDeleted)
                        {
                            Console.WriteLine("Запись с указанными данными не найдена");
                            Console.WriteLine(new string('-', 50));
                        }
                        notebook.SaveTextFile();
                        break;
                    case "3":
                        Console.WriteLine("Введите данные записи для редактирования:");
                        string identificationDataToEdit = Console.ReadLine();
                        // Поиск записи по идентификационным данным и редактирование

                        foreach (User userEdit in notebook.Users)
                        {
                            Recording recording = userEdit.GetRecordingByIdentificationData(identificationDataToEdit);
                            if (recording != null)
                            {
                                Console.WriteLine("Введите новые данные для записи:");
                                foreach (Recording record in userEdit.records)
                                {
                                    Console.WriteLine(new string('-', 50));
                                    Console.WriteLine($"\tИдентификационные данные: {record.IdentificationData}");
                                    Console.WriteLine($"\tТелефон: {record.ContactData.Phone}");
                                    Console.WriteLine($"\tАдрес: {record.ContactData.Adress}");
                                    Console.WriteLine($"\tПримечание: {record.Note}");
                                    Console.WriteLine(new string('-', 50));
                                }
                                
                                Console.Write("Введите ФИО: ");
                                string newIdentificationData = Console.ReadLine();

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

                                Console.Write("Введите адрес: ");
                                string newAdress = Console.ReadLine();

                                Console.Write("Введите текст заметки: ");
                                string newNote = Console.ReadLine();
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
                                    IdentificationData = newIdentificationData,
                                    ContactData = newContactData,
                                    Note = newNote

                                };
                                userEdit.EditRecord(recording, newEditRecording);
                                Console.WriteLine("Запись отредактирована");
                                break;
                            }
                        }
                        notebook.SaveTextFile();
                        break;
                    case "4":

                        notebook.SortUsersAndRecordings();
                        notebook.GetAllRecordings();
                        break;

                    case "5":
                        Environment.Exit(1);
                        return;
                    default:
                        Console.WriteLine("Некорректный выбор. Попробуйте еще раз.");
                        break;
                }
            }
        }
    }
}
