using Sticky.Notes.Application.Responses;

namespace Sticky.Notes.Application.Features.Notes.Commands.CreateNote
{
    public class CreateNoteCommandResponse : BaseResponse
    {
        public CreateNoteCommandResponse() : base()
        {

        }

        public CreateNoteDto Note { get; set; }
    }
}