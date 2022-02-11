using System;

namespace Sticky.Notes.Application.Features.Notes.Queries.GetNotesList
{
    public class NoteListViewModel
    {
        public Guid NoteId { get; set; }
        public string NoteName { get; set; }
        public string NoteDetails { get; set; }
        public string NoteColorCode { get; set; }
        public DateTime CreatedDate { get; set; }

    }
}
