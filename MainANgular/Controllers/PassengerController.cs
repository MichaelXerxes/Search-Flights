using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MainANgular.Dtos;
using MainANgular.ReadModel;
using MainANgular.Domain.Entities;
using MainANgular.Data;

namespace MainANgular.Controllers
{
    //[Route("api/[controller]")]
    [Route("[controller]")]
    [ApiController]
    public class PassengerController : ControllerBase
    {
        private readonly Entities _entities;
        public PassengerController(Entities entities)
        {
            this._entities = entities;
        }
        [HttpPost]
        [ProducesResponseType(201)]//201 for POst 200  for Get
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public IActionResult Register(NewPassenger dto)
        {
            _entities.Passengers.Add(new Passenger(
                dto.Email,
                dto.FirstName,
                dto.LastName,
                dto.Gnder));
            //Save in db
            _entities.SaveChanges();
            //System.Diagnostics.Debug.WriteLine(_entities.Passengers.Count);
            return CreatedAtAction(nameof(Find), new {email=dto.Email});  //Ok();  <-still is ok
        }
        [HttpGet("{email}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public ActionResult<PassengerRm> Find(string email)
        {
            var passenger = _entities.Passengers.FirstOrDefault(x => x.Email == email);
            if (passenger==null)
                 return NotFound();
            var rm = new PassengerRm(
                passenger.Email,
                passenger.FirstName,
                passenger.LastName,
                passenger.Gnder
                );

            return Ok(rm);
        }
    }
}
