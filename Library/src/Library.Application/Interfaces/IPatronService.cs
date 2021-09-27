using Library.Application.Dtos.Patrons;
using Library.SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Application.Interfaces
{
    public interface IPatronService
    {
        Task<List<PatronResponse>> GetPatrons(ResourceParameters resourceParameters);

        PatronAddResponse AddPatron(PatronAddRequest request);
    }
}
