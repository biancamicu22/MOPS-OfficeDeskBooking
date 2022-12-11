using Microsoft.AspNetCore.Mvc;
using System;
using DataLibrary.Models;
using AutoMapper;
using DataLibrary.DTO;
using MOPSAPI.Validations;
using MOPSAPI.Repository.Booking;

namespace MOPSAPI.Controllers
{
    [Route("api/v1/booking")]
    [ApiController]
    public class BookingController : Controller
    {
        private IBookingRepository _bookingRepository;

        public IEntityUpdateHandler EntityUpdateHandler { get; }

        public BookingController(IBookingRepository bookingRepository, IMapper autoMapper, IEntityUpdateHandler entityUpdateHandler)
        {
            _bookingRepository = bookingRepository;
            EntityUpdateHandler = entityUpdateHandler;
        }

        [HttpPost]
        public IActionResult Create([FromBody] BookingDTO booking)
        {
            try
            {
                return Ok(BookingDTO.FromModel(_bookingRepository.AddBookingRandomPlace(booking)));
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }
        [HttpPut]
        public IActionResult Update([FromBody] BookingDTO booking)
        {
            try
            {
                if (booking == null) throw new ArgumentNullException();
                return EntityUpdateHandler.Update<Booking>(booking.ToModel()).ToHttpResponse();
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        [HttpDelete]
        [Route("{id}")]
        public IActionResult Delete(string id)
        {
            try
            {
                if (string.IsNullOrEmpty(id)) throw new ArgumentNullException();
                return Ok(_bookingRepository.Delete(id));
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetById([FromQuery] string id)
        {
            try
            {
                if (string.IsNullOrEmpty(id)) throw new ArgumentNullException();
                return Ok(BookingDTO.FromModel(_bookingRepository.GetById(id)));
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        [HttpGet]
        [Route("all")]
        public IActionResult GetAll()
        {
            try
            {
                return Ok(_bookingRepository.GetAll());
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }
    }
}
