using NoteNote.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteNote.ViewModels
{
    public class NoteModelView : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<NoteModel> Notes { get; set; }
        private List<NoteModel> NoteList = new List<NoteModel>();

        private NoteModel CurrentNote;
        public string NoteBody;

        private string searchFilter;

        public NoteModelView()
        {
            NoteModel testOne = new NoteModel("Title One", "Title One Body!");
            NoteModel testTwo = new NoteModel("Title Twp", "Title Two Body!");

            NoteList.Add(testOne);
            NoteList.Add(testTwo);
        }

        public NoteModel SelectedNote
        {
            get { return CurrentNote; }
            set
            {
                CurrentNote = value;
                if (value == null)
                {
                    NoteBody = "Select a note";
                }
                else
                {
                    NoteBody = value.NoteBody;
                }
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("NoteBody"));
            }
        }

        public string Filter
        {
            get { return searchFilter; }
            set
            {
                if (value == searchFilter) { return; }
                searchFilter = value;
                PerformFiltering();
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Filter)));
            }
        }

        private void PerformFiltering()
        {
            if (searchFilter == null)
            {
                searchFilter = "";
            }
            // If there is something in the search bar, convert the stirng to lower case and trim white space on ends
            var lowerCaseFilter = Filter.ToLowerInvariant().Trim();

            //Get all notes that match the current search string, place them in a list
            var result =
                NoteList.Where(d => d.NoteAsString.ToLowerInvariant()
                .Contains(lowerCaseFilter))
                .ToList();

            //Create a list of Notes that do not match the search string 
            var toRemove = Notes.Except(result).ToList();

            // Remove the notes that do not match
            foreach (var x in toRemove)
            {
                Notes.Remove(x);
            }

            var resultCount = result.Count;
            // Add back in correct order.
            for (int i = 0; i < resultCount; i++)
            {
                var resultItem = result[i];
                if (i + 1 > Notes.Count || !Notes[i].Equals(resultItem))
                {
                    Notes.Insert(i, resultItem);
                }
            }
        }
    }
}
