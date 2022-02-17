using AutoMapper;
using MediatR;
using Sticky.Notes.Application.Contracts.Infrastructure;
using Sticky.Notes.Application.Contracts.Persistence;
using Sticky.Notes.Application.Models.Mail;
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
        private readonly IEmailService _emailService;

        public CreateNotesCommandHandler(IMapper mapper, INoteRepository noteRepository, IEmailService emailService)
        {
            _mapper = mapper;
            _noteRepository = noteRepository;
            _emailService = emailService;
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

            //Sending email notification to admin address
            var email = new Email() { To = "gill@snowball.be", Body = $"A new note was created: {request}", Subject = "A new note was created" };

            try
            {
                await _emailService.SendEmail(email);
            }
            catch (Exception ex)
            {
                //this shouldn't stop the API from doing else so this can be logged
                //_logger.LogError($"Mailing about event {@event.EventId} failed due to an error with the mail service: {ex.Message}");
            }

            return response;
        }
    }
}