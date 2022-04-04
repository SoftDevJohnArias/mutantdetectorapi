using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MutantDetector.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MutantDetector.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class statsController : ControllerBase
    {
        private readonly IDNAResultRepository _resultRepository;

    
        public statsController(IDNAResultRepository repository)
        {
            _resultRepository = repository;
        }
        


        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetStatus()
        {
            var resultado = _resultRepository.getResult();
            return new JsonResult(resultado);
        }

    }
}
