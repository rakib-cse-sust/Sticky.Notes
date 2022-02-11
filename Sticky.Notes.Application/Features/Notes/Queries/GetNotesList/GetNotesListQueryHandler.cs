using AutoMapper;
using MediatR;
using Sticky.Notes.Application.Contracts.Persistence;
using Sticky.Notes.Domain.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Sticky.Notes.Application.Features.Notes.Queries.GetNotesList
{
    public class GetNotesListQueryHandler : IRequestHandler<GetNotesListQuery, List<NoteListViewModel>>
    {
        private readonly IMapper _mapper;
        private readonly IAsyncRepository<Note> _noteRepository;

        public GetNotesListQueryHandler(IMapper mapper, IAsyncRepository<Note> noteRepository)
        {
            _mapper = mapper;
            _noteRepository = noteRepository;
        }

        public async Task<List<NoteListViewModel>> Handle(GetNotesListQuery request, CancellationToken cancellationToken)
        {
            var allnotes = (await _noteRepository.ListAllAsync()).OrderBy(x => x.CreatedDate);
            return _mapper.Map<List<NoteListViewModel>>(allnotes);
        }
    }
}
