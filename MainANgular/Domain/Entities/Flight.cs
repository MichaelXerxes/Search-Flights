using MainANgular.Domain.Errors;

namespace MainANgular.Domain.Entities;

public class Flight
{
    public Guid Id { get; set; }
    public string Airline { get; set; }
    public string Price { get; set; }
    public TimePlace Deprature { get; set; }
    public TimePlace Arrival { get; set; }
    public int RemainingNumberOfSetas { get; set; }
    public IList<Booking> Bookings = new List<Booking>();
    public Flight(Guid id,
            string airline,
            string price,
            TimePlace deprature,
            TimePlace arrival,
            int remainingNumberOfSetas)
    {
        Id = id;
        Airline = airline;
        Price = price;
        Deprature = deprature;
        Arrival = arrival;
        RemainingNumberOfSetas = remainingNumberOfSetas;



    }
    public Flight()
    {

    }
   
    

    public object? MakeBooking(string passengerEmail,byte numberOfSeats)
    {
        var flight = this;
        if (flight.RemainingNumberOfSetas < numberOfSeats)
        {
            return new OverbookError();
        }

        flight.Bookings.Add(new Booking(
            //dto.FlightId,
            passengerEmail,
            numberOfSeats));

        flight.RemainingNumberOfSetas -= numberOfSeats;

        return null;
    }
    public object? CancelBooking(string passengerEmail, byte numberOfSeats)
    {
        return null;
    }
}

