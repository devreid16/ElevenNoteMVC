using ElevenNote.Data;
using ElevenNote.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevenNote.Services
{
   public class NoteService
    {
        //capture user id
        private readonly Guid _userId;
        
        public NoteService(Guid userId)
        {
            _userId = userId;
        }
        /// <summary>
        /// Get all notes for the current user
        /// </summary>
        /// <returns>The current user's notes.</returns>
        
        public IEnumerable<NoteListItemModel> GetNotes()
        {
            using (var context = new ElevenNoteDBContext())
            {
                var query = context.Notes.Where(e => e.OwnerId == _userId)
                                         .Select(
                                              e =>
                                               new NoteListItemModel
                                              {
                                                NoteId = e.NoteId,
                                                Title = e.Title,
                                                CreatedUtc = e.CreatedUtc
                                              });
                        
                return query.ToArray();
            }
        }
        /// <summary>
        /// Create a new note for the current user
        /// </summary>
        /// <param name="model">The model to base the new note upon</param>
        /// <returns>A boolean indicating whether creating note was successful.</returns>

        public bool CreateNote(NoteCreateModel model)
        {
            var entity = 
                new NoteEntity
                {
                    OwnerId = _userId,
                    Title = model.Content,
                    Content = model.Content,
                    CreatedUtc = DateTimeOffset.UtcNow
                };
            //constructor for context
            using (var context = new ElevenNoteDBContext())
            {
                context.Notes.Add(entity);

                return context.SaveChanges() == 1;

            }
                
        }

        /// <summary>
        /// Gets a particular note for the current user
        /// </summary>
        /// <param name="noteId">The id of the note to retrieve</param>
        /// <returns>The specified note</returns>
        public NoteDetailModel GetNoteById(int noteId)
        {
            using (var context = new ElevenNoteDBContext())
            {
                var entity =
                    context.Notes
                           .Single(e => e.NoteId == noteId && e.OwnerId == _userId);

                return
                    new NoteDetailModel
                    {
                        NoteId = entity.NoteId,
                        Title = entity.Title,
                        Content = entity.Content,
                        CreateUtc = entity.CreatedUtc,
                        ModifiedUtc = entity.ModifiedUtc
                    };
            }
        }
        /// <summary>
        /// Update an existing note for current user
        /// </summary>
        /// <param name="model"></param>
        /// <returns>Updated note for current user</returns>
        public bool UpdateNote(NoteEditModel model)
        {
            using (var context = new ElevenNoteDBContext())
            {
                var entity =
                    context.Notes.Single(e => e.NoteId == model.NoteId && e.OwnerId == _userId);

                entity.Title = model.Title;
                entity.Content = model.Content;
                entity.ModifiedUtc = DateTimeOffset.UtcNow;

                return context.SaveChanges() == 1;
            }
        }

        public bool DeleteNote(int noteId)
        {
            using (var context = new ElevenNoteDBContext())
            {
                var entity =
                    context
                        .Notes
                        .Single(e => e.NoteId == noteId && e.OwnerId == _userId);

                context.Notes.Remove(entity);

                return context.SaveChanges() == 1;
            }
        }

    }
}
