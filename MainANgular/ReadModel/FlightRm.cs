namespace MainANgular.ReadModel
{
    public record FlightRm(
        Guid Id,
        string airline,
        string Price,
        TimePlaceRm Deprature,
        TimePlaceRm Arrival,
        int RemainingNumberOfSetas
        );
    
}
