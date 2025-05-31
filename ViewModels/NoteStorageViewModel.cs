using FlowerShop.Models;
using FlowerShop.Persistence;
using FlowerShop.Services.RepoServices;
using FlowerShop.Stores;
using FlowerShop.ViewModels.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using FlowerShop.Services;

namespace FlowerShop.ViewModels
{
    public class NoteStorageViewModel : BaseViewModel
    {
        public ICommand BackwardCommand { get; }
        public ICommand DeleteNoteCommand { get; }
        public ICommand ForwardToCreateNoteCommand { get; }
        public ICommand ForwardToEditNoteCommand { get; }

        private Note _selectedNote;
        public Note SelectedNote
        {
            get
            {
                return _selectedNote;
            }
            set
            {
                _selectedNote = value;
            }
        }

        private IRepo<Note> _noteRepo;
        private readonly NoteService _noteService;
        private readonly NavigationStore _navigationStore;
        public ObservableCollection<Note> Notes { get; set; } = new ObservableCollection<Note>();

        public NoteStorageViewModel(NavigationStore navigationStore)
        {
            _navigationStore = navigationStore;

            _noteRepo = (NoteRepo)App.RepoReg.Get<Note>("NoteRepo");
            _noteService = new NoteService(_noteRepo);

            ForwardToEditNoteCommand = new CommandHandler(() => _navigationStore.CurrentViewModel = new NoteEditViewModel(navigationStore, SelectedNote), () => SelectedNote != null);
            DeleteNoteCommand = new CommandHandler(DeleteSelectedNote, () => SelectedNote != null);
            ForwardToCreateNoteCommand = new NavigateCommand(new NavigationService(navigationStore, () => new NoteCreateViewModel(navigationStore)));

            LoadNotes();
        }

        public void LoadNotes()
        {
            try
            {
                Notes.Clear();
                var notes = _noteRepo.GetAll();
                foreach (var note in notes)
                {
                    Notes.Add(note);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Der opstod en fejl under indlæsning af noter: {ex.Message}", "Fejl", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public void DeleteSelectedNote()
        {
            if (MessageBox.Show("Er du sikker på, du vil slette? " + SelectedNote.Title, "Slet", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    _noteService.Remove(SelectedNote);
                    Notes.Remove(SelectedNote);
                    SelectedNote = null;
                    MessageBox.Show("Noten er nu blevet slettet", "Slet", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Der opstod en fejl: {ex.Message}", "Fejl", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
    }
}
