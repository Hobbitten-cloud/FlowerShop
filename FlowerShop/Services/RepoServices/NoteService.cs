using FlowerShop.Models;
using FlowerShop.Persistence;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlowerShop.Services.RepoServices
{
    public class NoteService
    {
        private readonly IRepo<Note> _noteRepo;

        public NoteService(IRepo<Note> noteRepo)
        {
            _noteRepo = noteRepo;
        }

        public void Add(Note note, string image)
        {
            if (string.IsNullOrWhiteSpace(note.Title))
            {
                throw new Exception("Titel er påkrævet.");
            }

            if (string.IsNullOrWhiteSpace(note.Description))
            {
                throw new Exception("Beskrivelse er påkrævet.");
            }

            if (!string.IsNullOrEmpty(image))
            {
                if (!File.Exists(image))
                {
                    throw new FileNotFoundException("Den angivne billedfil blev ikke fundet.", image);
                }

                note.Picture = File.ReadAllBytes(image);
            }

            if (note.Picture == null || note.Picture.Length == 0)
            {
                throw new Exception("Billede er påkrævet.");
            }

            _noteRepo.Add(note);
        }

        public void Update(Note note, string image)
        {
            if (string.IsNullOrWhiteSpace(note.Title))
            {
                throw new Exception("Titel er påkrævet.");
            }

            if (string.IsNullOrWhiteSpace(note.Description))
            {
                throw new Exception("Beskrivelse er påkrævet.");
            }

            if (!string.IsNullOrEmpty(image))
            {
                if (!File.Exists(image))
                {
                    throw new FileNotFoundException("Den angivne billedfil blev ikke fundet.", image);
                }

                note.Picture = File.ReadAllBytes(image);
            }

            if (note.Picture == null || note.Picture.Length == 0)
            {
                throw new Exception("Billede er påkrævet.");
            }

            _noteRepo.Update(note);
        }

        public void Remove(Note note)
        {
            if (note == null)
            {
                throw new Exception("Du skal vælge en note først for at slette.");
            }

            var existingNote = _noteRepo.GetById(note.Id);

            if (existingNote == null)
            {
                throw new Exception("Noten blev ikke fundet i databasen.");
            }

            _noteRepo.Remove(existingNote);
        }
    }
}
