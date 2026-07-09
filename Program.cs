namespace flight_project
{
    internal class Program
    {
        static string filePath = "flights.txt";
        static List<Flights> flights = new List<Flights>();

        static void Main(string[] args)
        {
            LoadFlightsFromFile();

            while (true)
            {
                    Console.WriteLine("--- МЕНЮ УПРАВЛЕНИЕ НА ПОЛЕТИ ---");
                    Console.WriteLine("1.Добавяне на нов полет ");
                    Console.WriteLine("2.Продажба на билети за полет ");
                    Console.WriteLine("3.Проверка на наличност на билети за полет ");
                    Console.WriteLine("4.Справка за всички полети ");
                    Console.WriteLine("5. Изход");
                    Console.Write("Изберете опция (1-5): ");

                    string choice = Console.ReadLine();

                    if (choice == "1")
                    {
                        AddNewFlight();
                    }
                    else if (choice == "2")
                    {
                        SellTickets();
                    }
                    else if (choice == "3")
                    {
                        CheckAvailability();
                    }
                    else if (choice == "4")
                    {
                        ShowAllFlights(); 
                    }
                    else if (choice == "5")
                    {
                        Console.WriteLine("Благодарим ви, че използвахте програмата. Приятен ден!");
                        break; 
                    }
                    else
                    {
                        Console.WriteLine("Невалиден избор. Моля, опитайте отново.");
                    }
                
            }

        }
         static void LoadFlightsFromFile()
        {
            if (!File.Exists(filePath))
            {
                File.Create(filePath).Close();
                return;
            }

            string[] lines = File.ReadAllLines(filePath);
            for (int i = 0; i < lines.Length; i++)
            {
                if (string.IsNullOrWhiteSpace(lines[i])) continue;

                string[] parts = lines[i].Split(',');
                if (parts.Length == 6)
                {
                    string id = parts[0];
                    string dest = parts[1];
                    string dep = parts[2];
                    string arr = parts[3];
                    int seats = int.Parse(parts[4]);
                    double price = double.Parse(parts[5]);

                    Flights f = new Flights(id, dest, dep, arr, seats, price);
                    flights.Add(f);
                }
            }
        }
    }
}
