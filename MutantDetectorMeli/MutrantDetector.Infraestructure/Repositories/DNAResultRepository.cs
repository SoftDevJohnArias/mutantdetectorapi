using MutantDetector.Core.Entities;
using MutantDetector.Core.Interfaces;
using MutantDetector.Core.Services;
using MutantDetector.Infraestructure.Data;
using System;
using System.Linq;

namespace MutantDetector.Infraestructure.Repositories
{
    public class DNAResultRepository : IDNAResultRepository
    {


        private readonly mutantdnaContext _context;

     //   public string DNA { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        
     
        public DNAResultRepository(mutantdnaContext context)
        {
            _context = context;
        }

        public Result getResult()
        {
            var count_human_dna = _context.Dna.Count(u => u.EsMutante == false);
            var count_mutant_dna = _context.Dna.Count(u => u.EsMutante == true);

            try
            {
            
            var resultados = new Result()
            {
                count_human_dna= count_human_dna,
                count_mutant_dna = count_mutant_dna,
                ratio = (decimal)((count_mutant_dna * 100 )/ count_human_dna)/100
            };
                return resultados;
            }
            catch (Exception ex)
            {
                // handle exception here
                var resultados = new Result()
                {
                    count_human_dna = count_human_dna,
                    count_mutant_dna = count_mutant_dna,
                    ratio = 0
                };
                return resultados;
            }

      
        }


        public void saveResult(object resultado)
        {
            _context.Add(resultado);
            _context.SaveChanges();    
        }
   
    }
}
