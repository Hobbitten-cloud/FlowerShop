using FlowerShop.Models;
using FlowerShop.Persistence;
using FlowerShop.Persistence.Interfaces;
using FlowerShop.Services;
using FlowerShop.Services.RepoServices;
using FlowerShop.Stores;
using FlowerShop.ViewModels.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace FlowerShop.ViewModels
{
    public class NoteEditViewModel : BaseViewModel, IPictureSelection
    {
        public ICommand SelectImageCommand { get; }
        public ICommand ForwardCommand { get; }
        public ICommand BackwardCommand { get; }
        public ICommand UpdateNoteCommand { get; }

        private string _selectedPicture;
        public string SelectedPicture
        {
            get
            {
                return _selectedPicture;
            }
            set
            {
                _selectedPicture = value; OnPropertyChanged();
            }
        }

        private Note _selectedNote;
        public Note SelectedNote
        {
            get
            {
                return _selectedNote;
            }
            set
            {
                _selectedNote = value; OnPropertyChanged();
            }
        }

        private IRepo<Note> _noteRepo;
        private readonly NoteService _noteService;

        public NoteEditViewModel(NavigationStore navigationStore, Note note)
        {
            _noteRepo = (NoteRepo)App.RepoReg.Get<Note>("NoteRepo");
            _noteService = new NoteService(_noteRepo);

            SelectedNote = note;

            UpdateNoteCommand = new CommandHandler(() => { EditNote(); });
            ForwardCommand = new NavigateCommand(new NavigationService(navigationStore, () => new NoteStorageViewModel(navigationStore)));
            BackwardCommand = new NavigateCommand(new NavigationService(navigationStore, () => new NoteStorageViewModel(navigationStore)));

            SelectImageCommand = new SelectImageCommand(this);
        }

        public void EditNote()
        {
            try
            {
                _noteService.Update(SelectedNote, SelectedPicture);
                MessageBox.Show("Noten er blevet opdateret!", "Succes", MessageBoxButton.OK, MessageBoxImage.Information);
                ForwardCommand.Execute(null);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Der opstod en fejl: {ex.Message}", "Fejl", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
