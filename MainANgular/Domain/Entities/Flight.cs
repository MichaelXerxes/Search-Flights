using MainANgular.Domain.Errors;

namespace MainANgular.Domain.Entities;

public record Flight(
    Guid Id,
    string airline,
    string Price,
    TimePlace Deprature,
    TimePlace Arrival,
    int RemainingNumberOfSetas
    )
{
    public IList<Booking> Bookings = new List<Booking>();
    public int RemainingNumberOfSetas { get; set; } = RemainingNumberOfSetas;

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
}

