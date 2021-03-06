﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevenNote.Data
{
    //table is attribute in schema name space
    [Table("Note")]

    //These Entities define our note table
    public class NoteEntity
    {
        [Key]

        public int NoteId { get; set; }

        [Required]
        public Guid OwnerId { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Content { get; set; }

        [DefaultValue(false)]
        public bool IsStarred { get; set; }

        [Required]
        public DateTimeOffset CreatedUtc { get; set; }

        //the ? allows for nulls
        public DateTimeOffset? ModifiedUtc { get; set; }

        public override string ToString()
        {
            return $"[{NoteId}] {Title}";
        }
    }
}
