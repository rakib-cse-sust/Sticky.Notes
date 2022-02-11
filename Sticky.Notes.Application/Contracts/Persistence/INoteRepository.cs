using Sticky.Notes.Domain.Entities;
using System.Threading.Tasks;

namespace Sticky.Notes.Application.Contracts.Persistence
{
    public interface INoteRepository : IAsyncRepository<Note>
    {
        Task<bool> IsNoteNameUnique(string name);
    }
}