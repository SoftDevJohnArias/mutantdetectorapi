using Microsoft.AspNetCore.Mvc;
using MutantDetector.Api.Controllers;
using MutantDetector.Core.Interfaces;
using MutantDetector.Core.Services;
using MutantDetector.Infraestructure.Data;
using NUnit.Framework;
using System.Threading.Tasks;

namespace MutantDetector.Test
{
    public class Tests  : IDNAResultRepository
    {

        private readonly IDNAResultRepository _mutantRepository;
        private readonly mutantController _mutantController;


        public Tests(IDNAResultRepository mutantRepository)
        {
            _mutantRepository = mutantRepository;
           
        }

        public Tests()
        {
        }

        public Result getResult()
        {
            throw new System.NotImplementedException();
        }

        public void saveResult(object resultado)
        {
            throw new System.NotImplementedException();
        }

        [SetUp]
        public  void SetupAsync()
        {
    
        }


        [Test]
        public void TestNotMatches()
        {
            var data = new string[] { "ATACAA", "CAGTGC", "TTATGT", "AGGGAG", "CCCTAG", "TCACTG" };
            
            var resultadoinsert = new DNA
            {
                dna = data,
                esMutante = false
            };

           var valor = new mutantController(_mutantRepository);

            var result = (StatusCodeResult)valor.GetDna(resultadoinsert);
            Assert.AreEqual(Microsoft.AspNetCore.Http.StatusCodes.Status403Forbidden, result.StatusCode);
        }


        [Test]
        public void TestHorizontalDNA()
        {
            var data = new string[] { "ATACAA",
                                           "CAGTGC",
                                           "TTCTGG",
                                           "AGAAAG",
                                           "CCCCAG", /*este tiene ok*/
                                           "TCACTT"
            };

            var resultadoinsert = new DNA
            { dna = data };
            var result = (StatusCodeResult)_mutantController.GetDna(resultadoinsert);
            Assert.AreEqual(Microsoft.AspNetCore.Http.StatusCodes.Status200OK, result.StatusCode);   
        }



        [Test]
        public void TestVerticalDNA()
        {
            var data = new string[] {   "ATACAA",
                                        "CAGTGC",
                                        "TTCTGG",
                                        "AGAAAG",
                                        "TCCAAG",
                                        "TCACTG"  /*la ultima linea de los 4 ultimos arreglos tienen la secuencia de 4 G*/
            };

            var resultadoinsert = new DNA
            { dna = data };
            var result = (StatusCodeResult)_mutantController.GetDna(resultadoinsert);
            Assert.AreEqual(Microsoft.AspNetCore.Http.StatusCodes.Status200OK, result.StatusCode);
        }


        [Test]
        public void TestDiagDNA()
        {
            var data = new string[] {   "ATACAA",
                                           "CAGTGC",
                                           "TTATGC",
                                           "AGCAAA",
                                           "TCCAAA",
                                           "TCACTG"  /*la Diagona Principal tiene 4A*/
        };

            var resultadoinsert = new DNA
            { dna = data };
            var result = (StatusCodeResult)_mutantController.GetDna(resultadoinsert);
            Assert.AreEqual(Microsoft.AspNetCore.Http.StatusCodes.Status200OK, result.StatusCode);
        }


        [Test]
        public void TestDNASizeError()
        {
            var data = new string[] { "ATACAA",
                                        "CAGTGC",
                                        "TTATGC",
                                        "AGCAAA",
                                        "TCCAA", /*error debe ser una matriz cuadrada esta tiene solo 5 elementos. y la matriz es de [6,6]*/
                                        "TCACTG"
                                        };

            var resultadoinsert = new DNA
            { dna = data };
            var result = (StatusCodeResult)_mutantController.GetDna(resultadoinsert);
            Assert.AreEqual(Microsoft.AspNetCore.Http.StatusCodes.Status400BadRequest, result.StatusCode);
        }


        [Test]
        public void TestDNACharError()
        {
            var data = new string[] { "ATACAA",
                                        "CAGTGC",
                                        "TTATGC",
                                        "AGCAAA",
                                        "TCCAAX", /*error debe validar el caracter X que no es valido para la base DNA*/
                                        "TCACTG"
                                            };
            var resultadoinsert = new DNA
            { dna = data };
            var result = (StatusCodeResult)_mutantController.GetDna(resultadoinsert);
            Assert.AreEqual(Microsoft.AspNetCore.Http.StatusCodes.Status400BadRequest, result.StatusCode);
        }



        [Test]
        public void TestSequenceRequired()
        {
            var data = new string[] { };
            var resultadoinsert = new DNA
            { dna = data };
            var result = (StatusCodeResult)_mutantController.GetDna(resultadoinsert);
            Assert.AreEqual(Microsoft.AspNetCore.Http.StatusCodes.Status403Forbidden, result.StatusCode);
        }



    }
}