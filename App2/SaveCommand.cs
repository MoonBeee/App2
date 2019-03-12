using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NoteNote.ViewModels;
using Windows.UI.Xaml.Controls;

namespace NoteNote
{
    class SaveCommand
    {

        public event EventHandler CanExecuteChanged;
        private NoteModelView NoteView;

        public SaveCommand(NoteModelView ndvm)
        {
            this.NoteView = ndvm;
        }

        public bool CanExecute(object parameter)
        {
            return NoteView.SelectedNote != null;
        }

        public async void Execute(object parameter)
        {
            //Debug.WriteLine(ndvm.SelectedNameDay.FullDate + 
            //    " - This nameday was just saved!");

            SaveNoteDialog sn = new SaveNoteDialog();
            ContentDialogResult result = await snd.ShowAsync();

            if (result == ContentDialogResult.Primary)
            {
                //Save names here
                try
                {
                    Repositories.NameDaysRepo.SaveNamedaysToFile(ndvm.SelectedNameDay, snd.UserNote);

                    ContentDialog savedDialog = new ContentDialog()
                    {
                        Title = "Save Successful",
                        Content = "Names saved successfully to file, hurray!",
                        PrimaryButtonText = "OK"
                    };
                    await savedDialog.ShowAsync();
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("Error when saving to file");
                }
            }
        }

        public void FireCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
