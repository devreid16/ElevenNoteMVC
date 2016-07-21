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
        private readonly Guid _userId;

        public NoteService(Guid userId)
        {
            _userId = userId;
        }


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

            using (var context = new ElevenNoteDBContext())
            {
                context.Notes.Add(entity);

                return context.SaveChanges() == 1;

            }
                
        }

    }
}
