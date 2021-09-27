using Library.Application.Dtos.Authors;
using Library.SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Application.Interfaces
{
    public interface IAuthorService
    {
        Task<List<AuthorResponse>> GetAuthors(ResourceParameters resourceParameters);

        AuthorAddResponse AddAuthor(AuthorAddRequest request);
    }
}
