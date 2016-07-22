using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevenNote.Model
{
    public class NoteDetailModel
    {
        public int NoteId { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        [UIHint("Starred")]
        public bool IsStarred { get; set; }

        public DateTimeOffset CreateUtc { get; set; }

        public DateTimeOffset? ModifiedUtc { get; set; }

        //interpolation to get note and title
        public override string ToString()
        {
             return $"[{NoteId}] {Title}";
        }


}
}
