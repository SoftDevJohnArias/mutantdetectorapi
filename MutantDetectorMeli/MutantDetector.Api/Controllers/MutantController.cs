using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using MutantDetector.Core.Services;
using MutantDetector.Core.Interfaces;
using MutantDetector.Core.Entities;

namespace MutantDetector.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class mutantController : ControllerBase
    {   

        private readonly IDNAResultRepository _mutantRepository;

       
       public mutantController(IDNAResultRepository mutantRepository)
        {
            _mutantRepository = mutantRepository;
        }

        /// <summary>
        /// Valida informacion de cadena mutante
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /api/mutant
        ///     {
        ///      "dna": ["ATACAA","CAGTGC","TTATGG","AGAAAG","CCCTAA","TCACTG"]
        ///     }
        ///
        /// </remarks>
        /// <response code="200">Felicitaciones eres un mutante tienes una secuencia de cuatro letras iguales en tu base ADN </response>
        /// <response code="403">lo siento eres un simple mortal XD </response> 
        /// <response code="400">ups debes revisar tu base Nitrogenada  debera ser (A,T.C,G) o quien eres? :@ </response> 
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ResponseCache(Duration = 60)]
        public ActionResult GetDna([FromBody] DNA data)
        {

            if (!ModelState.IsValid)
                return BadRequest();
            
            if (!ValidaCadenaDNA(data.dna))
                return StatusCode(400);


            int matrizsize = data.dna.Length;
            char[,] datosmutante = creamutantarray(data);

            mutantADN validacioncadenavertical = new VerticalValidation();
            mutantADN validacioncadenahorizontal = new HorizontalValidation();
            mutantADN validacioncadenadiagonal = new DiagonalValidation();

            var t_vlidaver = Task.Factory.StartNew(() => validacioncadenavertical.ValidacionCadena(datosmutante, matrizsize));
            var t_vlidahor = Task.Factory.StartNew(() => validacioncadenahorizontal.ValidacionCadena(datosmutante, matrizsize));
            var t_vlidadiag = Task.Factory.StartNew(() => validacioncadenadiagonal.ValidacionCadena(datosmutante, matrizsize));
            Task.WaitAll(t_vlidahor, t_vlidaver, t_vlidadiag);


            if (t_vlidahor.Result || t_vlidaver.Result || t_vlidadiag.Result)
            {
                insertdna(data.dna[0], true);
                return StatusCode(200); 
            }
            else
                insertdna(data.dna[0], false);
                return StatusCode(403);

        }

        private void insertdna(string dna, bool isMutant)
        {
            var resultadoinsert= new Dna
            {
                Cadena = dna,
                EsMutante = isMutant
            };

            _mutantRepository.saveResult(resultadoinsert);
        }

        private char[,] creamutantarray(DNA data)
        {
            int row = 0;
            char[,] chararray = new char[data.dna.Length, data.dna.Length];
            foreach (var datas in data.dna)
            {
                if (datas.Length == data.dna.Length)
                {
                    int column = 0;
                    foreach (Char characterdara in datas)
                    {
                        chararray[row, column] = characterdara;
                        column++;
                    }
                    row++;
                }

            }

            return chararray;
        }

        public static bool ValidaCadenaDNA(string[] data)
        {
            bool BaseNitrogenada = true;
            var validacion = new List<char>() { 'A', 'T', 'G', 'C' };

            foreach (var cadena in data)
            {
                if (cadena.Length != data.Length)
                    return false;

                else if (BaseNitrogenada)
                    BaseNitrogenada = cadena.ToUpper().All(validacion.Contains);
            }
            return BaseNitrogenada;
        }


    }

}