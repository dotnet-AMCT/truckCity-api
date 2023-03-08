using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using truckCity_api.Data;
using truckCity_api.Models;
using truckCity_api.Models.Dto;
using truckCity_api.Repositories;

namespace truckCity_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TruckController : ControllerBase
    {
        private readonly ITruckRepository _iTruckRepository;
        protected ResponseDto _responseDto;

        public TruckController(ITruckRepository iTruckRepository)
        {
            _iTruckRepository = iTruckRepository;
            _responseDto = new ResponseDto();
        }

        // GET: api/Truck
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Truck>>> GetTrucks()
        {
            try
            {
                var truckDtoList = await _iTruckRepository.GetTrucks();
                _responseDto.Result = truckDtoList;
                _responseDto.DisplayMessage = "Truck's list";
            }
            catch (Exception e)
            {
                _responseDto.IsSuccess = false;
                _responseDto.ErrorsMessages = new List<string> { e.ToString() };
            }
            return Ok(_responseDto);
        }

        // GET: api/Truck/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Truck>> GetTruck(int id)
        {
            var truck = await _iTruckRepository.GetTruckById(id);
            if (truck == null)
            {
                _responseDto.IsSuccess = false;
                _responseDto.DisplayMessage = "This Truck does not exist";
                return NotFound(_responseDto);
            }
            _responseDto.Result = truck;
            _responseDto.DisplayMessage = "Truck's Information";
            return Ok(_responseDto);
        }

        // PUT: api/Truck/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTruck(int id, TruckDto truckDto)
        {
            try
            {
                TruckDto model = await _iTruckRepository.CreateUpdate(truckDto);
                _responseDto.Result = model;
                return Ok(_responseDto);

            }
            catch (Exception e)
            {
                _responseDto.IsSuccess = false;
                _responseDto.DisplayMessage = "Failed to update truck register";
                _responseDto.ErrorsMessages = new List<string> { e.ToString() };
                return BadRequest(_responseDto);
            }
        }

        // POST: api/Truck
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Truck>> PostTruck(TruckDto truckDto)
        {
            try
            {
                TruckDto model = await _iTruckRepository.CreateUpdate(truckDto);
                _responseDto.Result = model;
                return CreatedAtAction("GetTruck", new { id = model.Id }, _responseDto);
            }
            catch (Exception e)
            {
                _responseDto.IsSuccess = false;
                _responseDto.DisplayMessage = "Failed to create truck register";
                _responseDto.ErrorsMessages = new List<string> { e.ToString() };
                return BadRequest(_responseDto);
            }
        }

        // DELETE: api/Truck/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTruck(int id)
        {
            try
            {
                bool IsDeleted = await _iTruckRepository.DeleteTruck(id);
                if (IsDeleted)
                {
                    _responseDto.Result = IsDeleted;
                    _responseDto.DisplayMessage = "Truck register successfully deleted";
                    return Ok(_responseDto);
                }
                else
                {
                    _responseDto.IsSuccess = false;
                    _responseDto.DisplayMessage = "Failed to delete truck register";
                    return BadRequest(_responseDto);
                }
            }
            catch (Exception e)
            {
                _responseDto.IsSuccess = false;
                _responseDto.ErrorsMessages = new List<string> { e.ToString() };
                return BadRequest(_responseDto);
            }
        }
    }
}
