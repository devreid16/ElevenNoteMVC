using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevenNote.Model
{
    public class NoteEditModel
    {
        public int NoteId { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public bool IsStarred { get; set; }

        //interpolation to get note and title
        public override string ToString()
        {
            return $"[{NoteId}] {Title}";
        }

    }
}
