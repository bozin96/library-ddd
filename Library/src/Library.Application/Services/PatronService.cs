using AutoMapper;
using Library.Application.Dtos.Patrons;
using Library.Application.Interfaces;
using Library.Core.SyncedAggregate;
using Library.Core.SyncedAggregates.Commands.AddPatron;
using Library.Core.SyncedAggregates.Specifications;
using Library.SharedKernel;
using Library.SharedKernel.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Application.Services
{
    public class PatronService : IPatronService
    {
        private readonly IMapper _mapper;
        private readonly IMediatorHandler _mediator;
        private readonly IRepository<Patron> _patronRepository;


        public PatronService(
            IMapper mapper,
            IMediatorHandler mediator,
            IRepository<Patron> authorRepository)
        {
            _mapper = mapper;
            _mediator = mediator;
            _patronRepository = authorRepository;
        }


        public async Task<List<PatronResponse>> GetPatrons(ResourceParameters resourceParameters)
        {
            PaginatedPatronsSpec paginatedPatronsSpec = new PaginatedPatronsSpec(resourceParameters);
            var paginatedPatrons = await _patronRepository.ListAsync(paginatedPatronsSpec);
            var patrons = _mapper.Map<List<PatronResponse>>(paginatedPatrons);

            return patrons;
        }

        public PatronAddResponse AddPatron(PatronAddRequest request)
        {
            var patronAddCommand = _mapper.Map<AddPatronCommand>(request);
            var baseCommandResponse = _mediator.SendCommand(patronAddCommand).Result;
            var response = _mapper.Map<PatronAddResponse>(baseCommandResponse);

            return response;
        }
    }
}
