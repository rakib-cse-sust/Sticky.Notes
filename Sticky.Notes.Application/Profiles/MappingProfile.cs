using AutoMapper;
using Sticky.Notes.Application.Features.Notes.Commands.CreateNote;
using Sticky.Notes.Application.Features.Notes.Commands.UpdateNote;
using Sticky.Notes.Application.Features.Notes.Queries.GetNotesList;
using Sticky.Notes.Domain.Entities;

namespace Sticky.Notes.Application.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Note, NoteListViewModel>().ReverseMap();
            CreateMap<Note, UpdateNoteCommand>().ReverseMap();
            CreateMap<Note, CreateNoteDto>().ReverseMap();
        }
    }
}
