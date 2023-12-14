namespace Notepade2
{
    class Program
    {
        static void Main(string[] args)
        {
            User user = new User();

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
                        // Читаем данные из консоли
                        // Создаем новую запись
                        // Вызываем метод AddRecording для добавления записи в список

                        Console.WriteLine(new string('-', 50));
                        Console.WriteLine("Введите данные для новой записи:");
                        Console.WriteLine(new string('-', 50));

                        Console.Write("Введите ФИО: ");
                        string identificationData = Console.ReadLine();

                        Console.Write("Введите телефонный номер: ");
                        string phone = Console.ReadLine();

                        Console.Write("Введите адрес: ");
                        string adress = Console.ReadLine();

                        Console.Write("Введите текст заметки: ");
                        string note = Console.ReadLine();

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


                        Console.WriteLine("Запись добавлена");
                        Console.WriteLine(new string('-', 50));
                        break;
                        
                    case "2":
                        Console.WriteLine("Введите данные записи для удаления:");
                        // Читаем данные из консоли
                        // Создаем объект типа Recording для поиска в списке
                        // Вызываем метод DeleteRecording для удаления записи из списка
                        break;
                    case "3":
                        Console.WriteLine("Введите данные записи для редактирования:");
                        // Читаем данные из консоли
                        // Создаем объект типа Recording для поиска в списке
                        // Console.WriteLine("Введите данные для новой записи:");
                        // Читаем новые данные из консоли
                        // Создаем объект типа Recording для замены старой записи
                        // Вызываем метод EditRecord для редактирования записи в списке
                        break;
                    case "4":

                        // Вызываем метод GetRecordings для вывода всех записей на консоль                                        
                        List<Recording> recordings = user.GetRecordings();
                        if (recordings.Count == 0)
                        {
                            Console.WriteLine("Нет доступных записей");
                        }
                        else
                        {
                            Console.WriteLine("Список записей (в алфавитном порядке):");
                            recordings = recordings.OrderBy(r => r.IdentificationData).ToList();
                            foreach (var recording in recordings)
                            {
                                Console.WriteLine(new string('-', 50));
                                Console.WriteLine($"ФИО: {recording.IdentificationData}");
                                Console.WriteLine($"Телефонный номер: {recording.ContactData.Phone}");
                                Console.WriteLine($"Адрес: {recording.ContactData.Adress}");
                                Console.WriteLine($"Запись: {recording.Note}");
                            }
                        }
                        break;
                        
                    case "5":
                        // Завершаем программу
                        return;
                    default:
                        Console.WriteLine("Некорректный выбор. Попробуйте еще раз.");
                        break;
                }
            }
        }
    }
}
