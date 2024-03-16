using MyProductivityWebApp.Data.Data;
using Microsoft.EntityFrameworkCore;

namespace MyProductivityWebApp.Data
{
    public class NoteService
    {
        private readonly MyProductivityWebAppContext _myProductivityWebAppContext;

        public NoteService(MyProductivityWebAppContext myProductivityWebAppContext)
        {
            _myProductivityWebAppContext = myProductivityWebAppContext;
        }

        public async Task<List<Note>> GetNotesAsync(string currentUser)
        {
            // Get note lists
            return await _myProductivityWebAppContext.Notes
                .Where(u => u.UserName == currentUser)
                .AsNoTracking().ToListAsync();
        }

        public Task<Note> CreateNotesAsync(Note note)
        {
            _myProductivityWebAppContext.Notes.Add(note);
            _myProductivityWebAppContext.SaveChanges();
            return Task.FromResult(note);
        }

        public Task<bool> UpdateNotesAsync(Note note)
        {
            var ExistingNote = _myProductivityWebAppContext.Notes
                .Where(n => n.Id == note.Id)
                .FirstOrDefault();

            if (ExistingNote != null)
            {
                ExistingNote.Title = note.Title;
                ExistingNote.Contents = note.Contents;
                _myProductivityWebAppContext.SaveChanges();
            }
            else
            {
                return Task.FromResult(false);
            }
            return Task.FromResult(true);
        }

        public Task<bool> DeleteNotesAsync(Note note)
        {
            var ExistingNote = _myProductivityWebAppContext.Notes
                .Where(n => n.Id == note.Id)
                .FirstOrDefault();

            if (ExistingNote != null)
            {
                _myProductivityWebAppContext.Notes.Remove(ExistingNote);
                _myProductivityWebAppContext.SaveChanges();
            }
            else
            {
                return Task.FromResult(false);
            }
            return Task.FromResult(true);
        }
    }
}
