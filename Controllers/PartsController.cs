﻿using Microsoft.AspNetCore.Mvc;
using truckCity_api.Repositories;
using truckCity_api.Models.Dto;

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
    }
}
