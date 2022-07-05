using MainANgular.Domain.Entities;

namespace MainANgular.Data
{
    public class Entities
    {
        static public IList<Passenger> Passengers = new List<Passenger>();
        static Random random= new Random();
        static public Flight[] Flights = new Flight[]
            {
               new( Guid.NewGuid(),
                   "Amercian Airlines",
                   random.Next(90,5000).ToString(),
                   new TimePlace("Los ANgeles",DateTime.Now.AddHours(random.Next(1,3))),
                   new TimePlace("Istambul",DateTime.Now.AddHours(random.Next(1,3))),
                   2),
               new( Guid.NewGuid(),
                   "AIr Lingus",
                   random.Next(90,5000).ToString(),
                   new TimePlace("London",DateTime.Now.AddHours(random.Next(1,3))),
                   new TimePlace("Dublin",DateTime.Now.AddHours(random.Next(1,3))),
                   random.Next(1,400)),
               new( Guid.NewGuid(),
                   "British Airlines",
                   random.Next(90,5000).ToString(),
                   new TimePlace("Manchaster",DateTime.Now.AddHours(random.Next(1,2))),
                   new TimePlace("Belfast",DateTime.Now.AddHours(random.Next(1,2))),
                   random.Next(1,400)),
               new( Guid.NewGuid(),
                   "Air Lingus",
                   random.Next(90,5000).ToString(),
                   new TimePlace("Los ANgeles",DateTime.Now.AddHours(random.Next(1,3))),
                   new TimePlace("Istambul",DateTime.Now.AddHours(random.Next(1,3))),
                   random.Next(1,400)),
               new( Guid.NewGuid(),
                   "AIr Poland",
                   random.Next(90,5000).ToString(),
                   new TimePlace("Derby",DateTime.Now.AddHours(random.Next(1,4))),
                   new TimePlace("Olsztyn",DateTime.Now.AddHours(random.Next(1,4))),
                   random.Next(1,400)),
               new( Guid.NewGuid(),
                   "Lufthansas",
                   random.Next(90,5000).ToString(),
                   new TimePlace("Krakow",DateTime.Now.AddHours(random.Next(1,3))),
                   new TimePlace("Wasraw",DateTime.Now.AddHours(random.Next(1,3))),
                   random.Next(1,400))
            };
    }
}
