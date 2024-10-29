namespace Assignment_Day4_part3
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Assignment 13
            /*
             Create a TransportSchedule class with properties like TransportType (bus or flight), Route, DepartureTime, ArrivalTime, Price, and SeatsAvailable.
                Create a TransportManager class to manage a list of transport schedules.
                LINQ Operations:
                   Search: Find schedules by transport type, route, or time.
                   Group By: Group schedules by transport type (bus or flight).
                   Order By: Order schedules by departure time, price, or seats available.
                   Filter: Filter schedules based on availability of seats or routes within a time range.
                   Aggregate: Calculate the total number of available seats and the average price of transport.
                   Select: Project a list of routes and their departure times.
             */
            TransportManager manager = new TransportManager();

            manager.AddSchedule(new TransportSchedule { TransportType = "bus", Route = "CityA-CityB", DepartureTime = DateTime.Now.AddHours(1), ArrivalTime = DateTime.Now.AddHours(4), Price = 15, SeatsAvailable = 40 });
            manager.AddSchedule(new TransportSchedule { TransportType = "flight", Route = "CityC-CityD", DepartureTime = DateTime.Now.AddHours(3), ArrivalTime = DateTime.Now.AddHours(5), Price = 150, SeatsAvailable = 20 });

            Console.WriteLine("Grouped by Transport type:");
            manager.DisplayGroupedByTransportType();

            Console.WriteLine("\n Order by Departure time:");
            foreach (var schedule in manager.OrderByDepartureTime())
                Console.WriteLine($"{schedule.TransportType} - {schedule.Route} - {schedule.DepartureTime}");

            Console.WriteLine("\nSchedule with 20 seats available:");
            foreach (var schedule in manager.FilterBySeats(20))
                Console.WriteLine($"{schedule.TransportType} - {schedule.Route} - {schedule.SeatsAvailable}");

            manager.DisplayTotalSeatsAndAveragePrice();
        }
    }
}
