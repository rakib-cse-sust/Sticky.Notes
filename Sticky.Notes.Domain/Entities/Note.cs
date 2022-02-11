using System;
using Sticky.Notes.Domain.Common;

namespace Sticky.Notes.Domain.Entities
{
    public class Note : AuditableEntity
    {
        public Guid NoteId { get; set; }
        public string NoteName { get; set; }
        public string NoteDetails { get; set; }
        public string NoteColorCode { get; set; }
    }
}