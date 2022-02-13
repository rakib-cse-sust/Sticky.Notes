using Sticky.Notes.Application.Contracts.Persistence;
using Sticky.Notes.Domain.Entities;
using System.Linq;
using System.Threading.Tasks;

namespace Sticky.Notes.Persistence.Repositories
{
    public class NoteRepository : BaseRepository<Note>, INoteRepository
    {
        public NoteRepository(StickyNotesDbContext dbContext) : base(dbContext)
        {
        }

        public Task<bool> IsNoteNameUnique(string name)
        {
            var matches = _dbContext.Notes.Any(e => e.NoteName.Equals(name));
            return Task.FromResult(matches);
        }
    }
}
