using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace flight_project
{
    internal class Flights
    {
         public string FlightId { get; set; }
         public string Destination { get; set; }
         public string DepartureTime { get; set; }
         public string ArrivalTime { get; set; }
         public int SeatsAvailable { get; set; }
         public double Price { get; set; }


         public Flight(string id, string dest, string dep, string arr, int seats, double price)
         {
             FlightId = id;
             Destination = dest;
             DepartureTime = dep;
             ArrivalTime = arr;
             SeatsAvailable = seats;
             Price = price;
         }
    }
}
