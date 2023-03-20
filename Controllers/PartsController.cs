using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using truckCity_api.Repositories;
using truckCity_api.Models;
using truckCity_api.Models.Dto;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Mvc.ApiExplorer;

namespace truckCity_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PartsController : ControllerBase
    {
        private readonly IPartRepository _partRepository;
        protected ResponseDto _responseDto;

        public PartsController(
            IPartRepository partRepository)
        {
            _partRepository = partRepository;
            _responseDto = new ResponseDto();
        }

        // GET: api/Parts
        [HttpGet]
        public async Task<ActionResult<ResponseDto>> GetPart()
        {
            try
            {
                var partList = await _partRepository.GetPart();
                _responseDto.Result = partList;
                _responseDto.DisplayMessage = "LIST OF PARTS";
                _responseDto.IsSuccess = true;
            }
            catch (Exception exception)
            {
                _responseDto.IsSuccess = false;
                _responseDto.DisplayMessage = "AN ERROR OCCURRED WHILE SEARCHING THE PARTS";
                _responseDto.ErrorMessages = new List<string>{ exception.ToString() };
            }
            
            return Ok(_responseDto);
        }

        // GET: api/Parts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ResponseDto>> GetPart(Guid id)
        {
            try
            {
                var part = await _partRepository.GetPart(id);
                _responseDto.Result = part;
                _responseDto.IsSuccess = _responseDto.Result != null;
            }
            catch (Exception exception) 
            {
                _responseDto.ErrorMessages = new List<string> { exception.ToString() };
            }

            if (!_responseDto.IsSuccess) 
            {
                _responseDto.DisplayMessage = "PART NOT FOUND";
                return NotFound(_responseDto);
            }
            _responseDto.DisplayMessage = "PART INFORMATION";

            return Ok(_responseDto);
        }

        // PUT: api/Parts/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPart(Guid id, UpdatePartDto partFields)
        {
            try
            {
                PartDto? partResult = await _partRepository.UpdatePart(id, partFields);
                _responseDto.Result = partResult;
            }
            catch (Exception exception)
            {
                _responseDto.Result = null;
                _responseDto.IsSuccess = false;
                _responseDto.DisplayMessage = "AN ERROR OCCURRED WHILE UPDATING THE PART";
                _responseDto.ErrorMessages = new List<string> { exception.ToString() };
            }
            
            if (_responseDto.Result != null)
            {
                _responseDto.IsSuccess = true;
                _responseDto.DisplayMessage = "PART UPDATED";
            }
            else
            {
                _responseDto.IsSuccess = false;
                _responseDto.DisplayMessage = "INVALID PARTID OR PART";
                return BadRequest(_responseDto);
            }

            return Ok(_responseDto);
        }

        // POST: api/Parts
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult> PostPart(CreatePartDto part)
        {
            try
            {
                PartDto? partResult = await _partRepository.CreatePart(part);
                _responseDto.Result = partResult;
                if (partResult != null)
                {
                    _responseDto.IsSuccess = true;
                    _responseDto.DisplayMessage = "PART ADDED";
                }
                else
                {
                    _responseDto.IsSuccess = false;
                    _responseDto.DisplayMessage = "INVALID PART";
                }
            }
            catch(Exception exception)
            {
                _responseDto.ErrorMessages = new List<string> { exception.ToString() };
                _responseDto.IsSuccess = false;
                _responseDto.DisplayMessage = "AN ERROR OCURRED WHILE ADDING THE PART";
            }

            if (!_responseDto.IsSuccess)
            {
                return BadRequest(_responseDto);
            }

            return Ok(_responseDto);
        }

        // DELETE: api/Parts/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePart(Guid id)
        {
            try
            {
                bool partIsDeleted = await _partRepository.DeletePart(id);
                if (partIsDeleted)
                {
                    _responseDto.Result = partIsDeleted;
                    _responseDto.IsSuccess = true;
                    _responseDto.DisplayMessage = "PART DELETED SUCCESFULLY";
                }
            }
            catch (Exception exception)
            {
                _responseDto.Result = false;
                _responseDto.IsSuccess = false;
                _responseDto.ErrorMessages = new List<string> { exception.ToString() };
                _responseDto.DisplayMessage = "AN ERROR OCCURRED WHILE TRYING TO DELETE THE PART";
            }

            if (_responseDto.IsSuccess is false)
            {
                return BadRequest(_responseDto);
            }
            
            return Ok(_responseDto);
        }

        [HttpGet("replacement/")]
        public async Task<IActionResult> GetReplacementParts([FromQuery]ReplacementPartsRequestDto replacementPartsRequest)
        {
            try 
            {
                var partList = await _partRepository.SearchPartsForReplacement(replacementPartsRequest.truckId, replacementPartsRequest.partCodes);
                if (partList != null)
                {
                    _responseDto.Result = partList;
                    _responseDto.DisplayMessage = "LIST OF PARTS";
                    _responseDto.IsSuccess = true;
                }
                else
                {
                    _responseDto.Result = null;
                    _responseDto.DisplayMessage = "INVALID TRUCKID OR NAME LIST";
                    _responseDto.IsSuccess = false;
                    return NotFound(_responseDto);
                }
            }
            catch (Exception exception)
            {
                _responseDto.Result = null;
                _responseDto.IsSuccess = false;
                _responseDto.ErrorMessages = new List<string> { exception.ToString() };
                _responseDto.DisplayMessage = "AN ERROR OCCURRED WHILE SEARCHING REPLACEMENT PARTS";
                return NotFound(_responseDto);
            }

            return Ok(_responseDto);
        }

        [HttpPut("assign/{id}")]
        public async Task<IActionResult> ModifyAssignationToTruckStatus(Guid id, Guid? truckId)
        {
            try
            {
                PartDto? partResult = await _partRepository.AssignOrUnassignTotruck(id, truckId);
                _responseDto.Result = partResult;
            }
            catch (Exception exception)
            {
                _responseDto.Result = null;
                _responseDto.IsSuccess = false;
                _responseDto.DisplayMessage = "AN ERROR OCCURRED WHILE ASSIGNING THE PART TO TRUCK";
                _responseDto.ErrorMessages = new List<string> { exception.ToString() };
            }

            if (_responseDto.Result != null)
            {
                _responseDto.IsSuccess = true;
                _responseDto.DisplayMessage = "PART ASSIGNED";
            }
            else
            {
                _responseDto.IsSuccess = false;
                _responseDto.DisplayMessage = "INVALID PARTID OR TRUCKID";
                return BadRequest(_responseDto);
            }

            return Ok(_responseDto);
        }

        [HttpGet("partStock/{code}")]
        public async Task<IActionResult> CheckPartStockByCode(string code)
        {
            try
            {
                var partStock = await _partRepository.SearchPartsByCode(code);
                if (partStock != null)
                {
                    _responseDto.DisplayMessage = "PART STOCK";
                }
                else
                {
                    _responseDto.DisplayMessage = "NO STOCK FOR THE GIVEN CODE";
                }
                _responseDto.Result = partStock;
                _responseDto.IsSuccess = true;
            }
            catch (Exception exception)
            {
                _responseDto.Result = null;
                _responseDto.IsSuccess = false;
                _responseDto.DisplayMessage = "AN ERROR OCCURRED WHILE SEARCHING FOR THE PART STOCK";
                _responseDto.ErrorMessages = new List<string> { exception.ToString() };

                return NotFound(_responseDto);
            }

            return Ok(_responseDto);
        }
    }
}
