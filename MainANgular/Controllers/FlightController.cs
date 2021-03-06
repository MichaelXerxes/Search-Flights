using MainANgular.ReadModel;
using Microsoft.AspNetCore.Mvc;
using MainANgular.Dtos;
using MainANgular.Domain.Entities;
using MainANgular.Domain.Errors;
using MainANgular.Data;
using Microsoft.EntityFrameworkCore;

namespace MainANgular.Controllers
{
    [ApiController]
    [Route("[controller]")]
    //[ProducesResponseType(400)] option for all end points
   // [ProducesResponseType(500)]
    public class FlightController : ControllerBase
    {
        private readonly Entities _entities;

        private readonly ILogger<FlightController> _logger;
        //static Random random = new Random();
        
        
    
        public FlightController(ILogger<FlightController> logger,
            Entities entities)
        {
            _logger = logger;
            this._entities = entities;
        }
        // public FlightRm Find(Guid id) 
        //   =>


        [HttpGet]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        [ProducesResponseType(typeof(IEnumerable<FlightRm>), 200)]
        public IEnumerable<FlightRm> Search()
        {       // convert data from Flight to FLightRm
            var flightRmList =_entities.Flights.Select(flight => new FlightRm(
                    flight.Id,
                    flight.Airline,
                    flight.Price,
                    new TimePlaceRm(flight.Deprature.Place.ToString(), flight.Deprature.Time),
                    new TimePlaceRm(flight.Arrival.Place.ToString(), flight.Arrival.Time),
                    flight.RemainingNumberOfSetas
                    ));//.ToArray();

            return flightRmList;
        }

        //   //
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        [ProducesResponseType(typeof(FlightRm),200)]

        public ActionResult<FlightRm> Find(Guid id)
        {
            var flight= _entities.Flights.SingleOrDefault(f => f.Id == id);
            if (flight==null)
                return NotFound();

            var readModel = new FlightRm(
                flight.Id,
                flight.Airline,
                flight.Price,
                new TimePlaceRm( flight.Deprature.Place.ToString(),flight.Deprature.Time),
                new TimePlaceRm(flight.Arrival.Place.ToString(), flight.Arrival.Time),
                flight.RemainingNumberOfSetas
                );
            return Ok(readModel);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public IActionResult Book(BookDto dto)
        {
            System.Diagnostics.Debug.WriteLine($"Booking a new flight {dto.FlightId}");
            var flight = _entities.Flights.SingleOrDefault(f => f.Id == dto.FlightId);
            if (flight==null)
                return NotFound();

            var error=flight.MakeBooking(dto.PassengerEmail,dto.NumberOfSeats);

            if (error is OverbookError)
                return Conflict(new { message = "Not enough seats!" });
            ///
            ///
            try
            {
                _entities.SaveChanges();
            }
            catch (DbUpdateConcurrencyException e)
            {

                return Conflict(new { message = "An error occured while booking. Pleas try again" });
            }
            ///
            return CreatedAtAction(nameof(Find), new { id = dto.FlightId });

        }
            
    }
}