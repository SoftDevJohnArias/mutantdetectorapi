using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MutantDetector.Core.Services
{
    public class DNA
    {

        [Required(ErrorMessage="no puedo validar si eres mutante, sin tu cadena de ADN")]
        public string[] dna { get; set; }
        public bool esMutante{ get; set; }

    }
}
