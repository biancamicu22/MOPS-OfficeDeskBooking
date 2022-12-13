using Microsoft.AspNetCore.Mvc;
using MOPSAPI.Repository;
using System;
using DataLibrary.Models;
using AutoMapper;
using DataLibrary.DTO;
using System.Linq;
using MOPSAPI.Validations;
using MOPSAPI.Repository.Desk;

namespace MOPSAPI.Controllers
{
    [Route("api/v1/desk")]
    [ApiController]
    public class DeskController : Controller
    {
        private IDeskRepository _deskRepository;

        public DeskController(IDeskRepository deskRepository)
        {
            _deskRepository = deskRepository;
            //EntityUpdateHandler = entityUpdateHandler;
        }

        [HttpPost]
        public IActionResult Create([FromBody] DeskDTO desk)
        {
            try
            {
                if(desk == null) throw new ArgumentNullException(nameof(desk));
                var atra = desk.ToModel();
                return Ok(DeskDTO.FromModel(_deskRepository.Add(atra)));
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        //[HttpPut]
        //public IActionResult Update([FromBody] DeskDTO desk)
        //{
        //    try
        //    {
        //        if (desk == null) throw new ArgumentNullException();
        //        return EntityUpdateHandler.Update<Desk>(desk.ToModel()).ToHttpResponse();
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest();
        //    }
        //}

        //[HttpDelete]
        //[Route("{id}")]
        //public IActionResult Delete(string id)
        //{
        //    try
        //    {
        //        if (string.IsNullOrEmpty(id)) throw new ArgumentNullException();
        //        return Ok(_deskRepository.Delete(id));
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest();
        //    }
        //}

        //[HttpGet("{id}")]
        //public IActionResult GetById([FromQuery] string id)
        //{
        //    try
        //    {
        //        if (string.IsNullOrEmpty(id)) throw new ArgumentNullException();
        //        return Ok(DeskDTO.FromModel(_deskRepository.GetById(id)));
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest();
        //    }
        //}

        [HttpGet]
        [Route("all")]
        public IActionResult GetAll()
        {
            return Ok(_deskRepository.GetAll());
        }
    }
}
