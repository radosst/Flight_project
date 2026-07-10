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
        static void AddNewFlight()
        {
            Console.WriteLine("--- ДОБАВЯНЕ НА НОВ ПОЛЕТ ---");
            Console.Write("Въведете ID на полета: ");
            string id = Console.ReadLine();

            Console.Write("Въведете дестинация: ");
            string dest = Console.ReadLine();

            Console.Write("Въведете време на излитане (напр. 12-12-24 12:00): ");
            string dep = Console.ReadLine();

            Console.Write("Въведете време на пристигане (напр. 12-12-24 12:30): ");
            string arr = Console.ReadLine();

            Console.Write("Въведете налични места: ");
            int seats = int.Parse(Console.ReadLine());

            Console.Write("Въведете цена на билет: ");
            double price = double.Parse(Console.ReadLine());

            Flights newFlight = new Flights(id, dest, dep, arr, seats, price);
            flights.Add(newFlight);

            SaveFlightsToFile();
            Console.WriteLine("Полетът е добавен успешно и файлът е актуализиран!");
        }

        static void SellTickets()
        {
            Console.WriteLine("\n--- ПРОДАЖБА НА БИЛЕТИ ---");
            Console.Write("Въведете ID на полета, за който купувате билети: ");
            string searchId = Console.ReadLine();

            Flight foundFlight = null;
            for (int i = 0; i < flights.Count; i++)
            {
                if (flights[i].FlightId == searchId)
                {
                    foundFlight = flights[i];
                    break;
                }
            }

            if (foundFlight == null)
            {
                Console.WriteLine("Полет с такова ID не беше намерен.");
                return;
            }

            Console.Write($"Налични места: {foundFlight.SeatsAvailable}. Колко билета искате да закупите? ");
            int countToBuy = int.Parse(Console.ReadLine());

            if (countToBuy <= foundFlight.SeatsAvailable)
            {
                double totalPrice = countToBuy * foundFlight.Price;
                foundFlight.SeatsAvailable -= countToBuy;
        
                SaveFlightsToFile();

                Console.WriteLine($"Успешна покупка! Обща стойност: {totalPrice:F2} евро.");
                Console.WriteLine($"Остават {foundFlight.SeatsAvailable} свободни места за този полет.");
            }
            else
              {
                Console.WriteLine("Няма достатъчно свободни места за тази поръчка!");
              }
        }    

        
        static void CheckAvailability()
        {
            Console.WriteLine("--- ПРОВЕРКА НА НАЛИЧНОСТ ---");
            Console.Write("Въведете дестинация или ID на полет за търсене: ");
            string searchInput = Console.ReadLine();

            bool found = false;
            for (int i = 0; i < flights.Count; i++)
            { 
                if (flights[i].FlightId == searchInput || flights[i].Destination.ToLower() == searchInput.ToLower())
                {
                    Console.WriteLine($"Намерено съвпадение:");
                    Console.WriteLine($"Дестинация: {flights[i].Destination} | Налични билети: {flights[i].SeatsAvailable} | Цена: {flights[i].Price:F2} евро.");
                    found = true;
                }
            }

            if (!found)
            {
                Console.WriteLine("Не е намерен полет по въведените критерии.");
            }
        }
    }
}
