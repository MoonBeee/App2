using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteNote.Models
{
    public class NoteModel
    {
        public string NoteTitle { get; }
        public string NoteBody { get; }

        public NoteModel (string noteTitle, string noteBody)
        {
            NoteTitle = noteTitle;
            NoteBody = noteBody;
        }

        public string NoteAsString => string.Join(", ", NoteTitle);
    }
}
