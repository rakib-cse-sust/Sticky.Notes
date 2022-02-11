using AutoMapper;
using MediatR;
using Sticky.Notes.Application.Contracts.Persistence;
using Sticky.Notes.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Sticky.Notes.Application.Features.Notes.Commands.CreateNote
{
    public class CreateNotesCommandHandler : IRequestHandler<CreateNoteCommand, Guid>
    {
        private readonly IMapper _mapper;
        private readonly INoteRepository _noteRepository;

        public CreateNotesCommandHandler(IMapper mapper, INoteRepository noteRepository)
        {
            _mapper = mapper;
            _noteRepository = noteRepository;
        }

        public async Task<Guid> Handle(CreateNoteCommand request, CancellationToken cancellationToken)
        {
            var validator = new CreateNoteCommandValidator(_noteRepository);
            var validationResult = await validator.ValidateAsync(request);

            if (validationResult.Errors.Count > 0)
                throw new Exceptions.ValidationException(validationResult);

            var @note = _mapper.Map<Note>(request);
            @note = await _noteRepository.AddAsync(@note);
            
            return @note.NoteId;
        }

        private async Task<CreateNoteCommandResponse> Handle1(CreateNoteCommand request, CancellationToken cancellationToken)
        {
            var response = new CreateNoteCommandResponse();
            var validator = new CreateNoteCommandValidator(_noteRepository);
            var validationResult = await validator.ValidateAsync(request);

            if (validationResult.Errors.Count > 0)
            {
                response.Success = false;
                response.ValidationErrors = new List<string>();

                foreach (var error in validationResult.Errors)
                {
                    response.ValidationErrors.Add(error.ErrorMessage);
                }
            }

            if (response.Success) 
            {
                var @note = _mapper.Map<Note>(request);
                @note = await _noteRepository.AddAsync(@note);

                response.Note = _mapper.Map<CreateNoteDto>(@note);
            }            
            
            return response;
        }
    }
}