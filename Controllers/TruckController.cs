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
    [Route("[controller]")]
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

        // GET: Truck
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

        // GET: Truck/5
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

        // PUT: Truck/5
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
            }




            //
            if (id != truck.Id)
            {
                return BadRequest();
            }

            _context.Entry(truck).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TruckExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: Truck
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Truck>> PostTruck(Truck truck)
        {
            _context.Trucks.Add(truck);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTruck", new { id = truck.Id }, truck);
        }

        // DELETE: Truck/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTruck(int id)
        {
            var truck = await _context.Trucks.FindAsync(id);
            if (truck == null)
            {
                return NotFound();
            }

            _context.Trucks.Remove(truck);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TruckExists(int id)
        {
            return _context.Trucks.Any(e => e.Id == id);
        }
    }
}
