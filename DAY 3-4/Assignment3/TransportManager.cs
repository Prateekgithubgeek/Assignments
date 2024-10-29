using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment_Day4_part3
{
    public class TransportManager
    {
        private List<TransportSchedule> schedules = new List<TransportSchedule>();

        public void AddSchedule(TransportSchedule schedule)
        {
            schedules.Add(schedule);
        }

        // Search Operation 
        public IEnumerable<TransportSchedule> Search(string transportType = null,string route = null)
        {
            return schedules.Where(s =>
            (transportType == null || s.TransportType == transportType) &&
            (route == null || s.Route == route));
        }

        // Group by Operation
        public void DisplayGroupedByTransportType()
        {
            var groups = schedules.GroupBy(s=>s.TransportType);
            foreach(var group in groups)
            {
                Console.WriteLine($"\n {group.Key} scheduls:");
                foreach(var schedule in group)
                {
                    Console.WriteLine($"{schedule.Route} - {schedule.DepartureTime}");

                }
            }
        }

        // Order by departure time operation 
        public IEnumerable<TransportSchedule> OrderByDepartureTime()
        {
            return schedules.OrderBy(s => s.DepartureTime);
        }

        // Filter by seat operation 
        public IEnumerable<TransportSchedule> FilterBySeats(int minSeats)
        {
            return schedules.Where(s => s.SeatsAvailable >= minSeats);
        }

        // Total available seats and average price
        public void DisplayTotalSeatsAndAveragePrice()
        {
            int totalSeats = schedules.Sum(s => s.SeatsAvailable);
            decimal averagePrice = schedules.Average(s => s.Price);
            Console.WriteLine($"\nTotal Seats: {totalSeats}, Average Price: {averagePrice:C}");
        }
    }
}
