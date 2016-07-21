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
        public IEnumerable<NoteListItemModel> GetNotes()
        {
            using (var context = new ElevenNoteDBContext())
            {
                var query = context.Notes.Select(
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

    }
}
