using MediatR;
using System.Collections.Generic;

namespace Sticky.Notes.Application.Features.Notes.Queries.GetNotesList
{
    public class GetNotesListQuery: IRequest<List<NoteListViewModel>>
    {
    }
}