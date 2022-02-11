using AutoMapper;
using MediatR;
using Sticky.Notes.Application.Contracts.Persistence;
using Sticky.Notes.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace Sticky.Notes.Application.Features.Notes.Commands.UpdateNote
{
    public class UpdateNoteCommandHandler : IRequestHandler<UpdateNoteCommand>
    {
        private readonly IMapper _mapper;
        private readonly IAsyncRepository<Note> _noteRepository;

        public UpdateNoteCommandHandler(IMapper mapper, IAsyncRepository<Note> noteRepository)
        {
            _mapper = mapper;
            _noteRepository = noteRepository;
        }

        public async Task<Unit> Handle(UpdateNoteCommand request, CancellationToken cancellationToken)
        {
            var noteToUpdate = await _noteRepository.GetByIdAsync(request.NoteId);
            _mapper.Map(request, noteToUpdate, typeof(UpdateNoteCommand), typeof(Note));
            await _noteRepository.UpdateAsync(noteToUpdate);

            return Unit.Value;
        }
    }
}
