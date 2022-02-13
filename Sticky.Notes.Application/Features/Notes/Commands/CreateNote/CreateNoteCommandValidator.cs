using FluentValidation;
using System.Threading;
using System.Threading.Tasks;
using Sticky.Notes.Application.Contracts.Persistence;

namespace Sticky.Notes.Application.Features.Notes.Commands.CreateNote
{
    public class CreateNoteCommandValidator : AbstractValidator<CreateNoteCommand>
    {
        private readonly INoteRepository _noteRepository;

        public CreateNoteCommandValidator(INoteRepository noteRepository)
        {
            _noteRepository = noteRepository;

            RuleFor(p => p.NoteName)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .MaximumLength(50).WithMessage("{PropertyName} must not exceed 50 characters.");

            RuleFor(e => e)
                .MustAsync(NoteNameUnique)
                .WithMessage("A note with the same name already exists.");

        }

        private async Task<bool> NoteNameUnique(CreateNoteCommand e, CancellationToken cancellationToken)
        {
            return !(await _noteRepository.IsNoteNameUnique(e.NoteName));
        }
    }
}