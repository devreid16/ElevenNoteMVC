using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevenNote.Model
{
    public class NoteCreateModel
    {
        [Required]
        [MinLength(2, ErrorMessage = "Please Enter at least 2 Characters")]
        [MaxLength(128)]

        public string Title { get; set; }

        public string Content { get; set; }

        public override string ToString()
        {
            return $"[New] {Title}";
        }

    }
}