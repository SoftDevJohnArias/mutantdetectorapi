using System;
using System.Collections.Generic;
using System.Text;

namespace MutantDetector.Core.Services
{
    public class Result
    {
        public int count_mutant_dna { get; set; }
        public int count_human_dna { get; set; }
        public decimal ratio { get; set; }

    }
}
