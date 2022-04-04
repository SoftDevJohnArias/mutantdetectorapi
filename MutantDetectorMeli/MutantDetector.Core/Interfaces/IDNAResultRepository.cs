using MutantDetector.Core.Entities;
using MutantDetector.Core.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MutantDetector.Core.Interfaces
{
    public interface IDNAResultRepository
    {
        Result getResult();
        void saveResult(object resultado);

    }
}
