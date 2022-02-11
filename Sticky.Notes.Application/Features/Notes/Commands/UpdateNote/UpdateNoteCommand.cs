using MediatR;
using System;

namespace Sticky.Notes.Application.Features.Notes.Commands.UpdateNote
{
    public class UpdateNoteCommand : IRequest
    {
        public Guid NoteId { get; set; }
        public string NoteName { get; set; }
        public string NoteDetails { get; set; }
        public string NoteColorCode { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
