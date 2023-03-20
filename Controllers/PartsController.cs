using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using truckCity_api.Repository;
using truckCity_api.Models;
using truckCity_api.Models.DTO;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Mvc.ApiExplorer;

namespace truckCity_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PartsController : ControllerBase
    {
        private readonly IPartRepository _partRepository;
        protected ResponseDTO _responseDTO;

        public PartsController(
            IPartRepository partRepository)
        {
            _partRepository = partRepository;
            _responseDTO = new ResponseDTO();
        }

        // GET: api/Parts
        [HttpGet]
        public async Task<ActionResult<ResponseDTO>> GetPart()
        {
            try
            {
                var partList = await _partRepository.GetPart();
                _responseDTO.Result = partList;
                _responseDTO.DisplayMessage = "LIST OF PARTS";
                _responseDTO.IsSuccess = true;
            }
            catch (Exception exception)
            {
                _responseDTO.IsSuccess = false;
                _responseDTO.DisplayMessage = "AN ERROR OCCURRED WHILE SEARCHING THE PARTS";
                _responseDTO.ErrorMessages = new List<string>{ exception.ToString() };
            }
            
            return Ok(_responseDTO);
        }

        // GET: api/Parts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ResponseDTO>> GetPart(Guid id)
        {
            try
            {
                var part = await _partRepository.GetPart(id);
                _responseDTO.Result = part;
                _responseDTO.IsSuccess = _responseDTO.Result != null;
            }
            catch (Exception exception) 
            {
                _responseDTO.ErrorMessages = new List<string> { exception.ToString() };
            }

            if (!_responseDTO.IsSuccess) 
            {
                _responseDTO.DisplayMessage = "PART NOT FOUND";
                return NotFound(_responseDTO);
            }
            _responseDTO.DisplayMessage = "PART INFORMATION";

            return Ok(_responseDTO);
        }

        // PUT: api/Parts/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPart(Guid id, UpdatePartDTO partFields)
        {
            try
            {
                PartDTO? partResult = await _partRepository.UpdatePart(id, partFields);
                _responseDTO.Result = partResult;
            }
            catch (Exception exception)
            {
                _responseDTO.Result = null;
                _responseDTO.IsSuccess = false;
                _responseDTO.DisplayMessage = "AN ERROR OCCURRED WHILE UPDATING THE PART";
                _responseDTO.ErrorMessages = new List<string> { exception.ToString() };
            }
            
            if (_responseDTO.Result != null)
            {
                _responseDTO.IsSuccess = true;
                _responseDTO.DisplayMessage = "PART UPDATED";
            }
            else
            {
                _responseDTO.IsSuccess = false;
                _responseDTO.DisplayMessage = "INVALID PARTID OR PART";
                return BadRequest(_responseDTO);
            }

            return Ok(_responseDTO);
        }

        // POST: api/Parts
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult> PostPart(CreatePartDTO part)
        {
            try
            {
                PartDTO? partResult = await _partRepository.CreatePart(part);
                _responseDTO.Result = partResult;
                if (partResult != null)
                {
                    _responseDTO.IsSuccess = true;
                    _responseDTO.DisplayMessage = "PART ADDED";
                }
                else
                {
                    _responseDTO.IsSuccess = false;
                    _responseDTO.DisplayMessage = "INVALID PART";
                }
            }
            catch(Exception exception)
            {
                _responseDTO.ErrorMessages = new List<string> { exception.ToString() };
                _responseDTO.IsSuccess = false;
                _responseDTO.DisplayMessage = "AN ERROR OCURRED WHILE ADDING THE PART";
            }

            if (!_responseDTO.IsSuccess)
            {
                return BadRequest(_responseDTO);
            }

            return Ok(_responseDTO);
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
                    _responseDTO.Result = partIsDeleted;
                    _responseDTO.IsSuccess = true;
                    _responseDTO.DisplayMessage = "PART DELETED SUCCESFULLY";
                }
            }
            catch (Exception exception)
            {
                _responseDTO.Result = false;
                _responseDTO.IsSuccess = false;
                _responseDTO.ErrorMessages = new List<string> { exception.ToString() };
                _responseDTO.DisplayMessage = "AN ERROR OCCURRED WHILE TRYING TO DELETE THE PART";
            }

            if (_responseDTO.IsSuccess is false)
            {
                return BadRequest(_responseDTO);
            }
            
            return Ok(_responseDTO);
        }

        [HttpGet("replacement/")]
        public async Task<IActionResult> GetReplacementParts([FromQuery]ReplacementPartsRequestDTO replacementPartsRequest)
        {
            try 
            {
                var partList = await _partRepository.SearchPartsForReplacement(replacementPartsRequest.truckId, replacementPartsRequest.partCodes);
                if (partList != null)
                {
                    _responseDTO.Result = partList;
                    _responseDTO.DisplayMessage = "LIST OF PARTS";
                    _responseDTO.IsSuccess = true;
                }
                else
                {
                    _responseDTO.Result = null;
                    _responseDTO.DisplayMessage = "INVALID TRUCKID OR NAME LIST";
                    _responseDTO.IsSuccess = false;
                    return NotFound(_responseDTO);
                }
            }
            catch (Exception exception)
            {
                _responseDTO.Result = null;
                _responseDTO.IsSuccess = false;
                _responseDTO.ErrorMessages = new List<string> { exception.ToString() };
                _responseDTO.DisplayMessage = "AN ERROR OCCURRED WHILE SEARCHING REPLACEMENT PARTS";
                return NotFound(_responseDTO);
            }

            return Ok(_responseDTO);
        }

        [HttpPut("assign/{id}")]
        public async Task<IActionResult> ModifyAssignationToTruckStatus(Guid id, Guid? truckId)
        {
            try
            {
                PartDTO? partResult = await _partRepository.AssignOrUnassignTotruck(id, truckId);
                _responseDTO.Result = partResult;
            }
            catch (Exception exception)
            {
                _responseDTO.Result = null;
                _responseDTO.IsSuccess = false;
                _responseDTO.DisplayMessage = "AN ERROR OCCURRED WHILE ASSIGNING THE PART TO TRUCK";
                _responseDTO.ErrorMessages = new List<string> { exception.ToString() };
            }

            if (_responseDTO.Result != null)
            {
                _responseDTO.IsSuccess = true;
                _responseDTO.DisplayMessage = "PART ASSIGNED";
            }
            else
            {
                _responseDTO.IsSuccess = false;
                _responseDTO.DisplayMessage = "INVALID PARTID OR TRUCKID";
                return BadRequest(_responseDTO);
            }

            return Ok(_responseDTO);
        }

        [HttpGet("partStock/{code}")]
        public async Task<IActionResult> CheckPartStockByCode(string code)
        {
            try
            {
                var partStock = await _partRepository.SearchPartsByCode(code);
                if (partStock != null)
                {
                    _responseDTO.DisplayMessage = "PART STOCK";
                }
                else
                {
                    _responseDTO.DisplayMessage = "NO STOCK FOR THE GIVEN CODE";
                }
                _responseDTO.Result = partStock;
                _responseDTO.IsSuccess = true;
            }
            catch (Exception exception)
            {
                _responseDTO.Result = null;
                _responseDTO.IsSuccess = false;
                _responseDTO.DisplayMessage = "AN ERROR OCCURRED WHILE SEARCHING FOR THE PART STOCK";
                _responseDTO.ErrorMessages = new List<string> { exception.ToString() };

                return NotFound(_responseDTO);
            }

            return Ok(_responseDTO);
        }
    }
}
