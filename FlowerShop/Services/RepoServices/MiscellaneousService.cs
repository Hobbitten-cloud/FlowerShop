using FlowerShop.Models;
using FlowerShop.Models.Enums;
using FlowerShop.Persistence;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlowerShop.Services.RepoServices
{
    public class MiscellaneousService
    {
        private readonly IRepo<Miscellaneous> _miscellaneousRepo;

        public MiscellaneousService(IRepo<Miscellaneous> miscellaneousRepo)
        {
            _miscellaneousRepo = miscellaneousRepo;
        }

        public void Add(Miscellaneous miscellaneous, string image)
        {
            if (string.IsNullOrWhiteSpace(miscellaneous.Name))
            {
                throw new Exception("Navn er påkrævet.");
            }

            if (miscellaneous.PurchasePrice <= 0)
            {
                throw new Exception("Indkøbspris skal være større end 0.");
            }

            if (miscellaneous.Amount <= 0)
            {
                throw new Exception("Antal skal være større end 0.");
            }

            if (!string.IsNullOrEmpty(image))
            {
                if (!File.Exists(image))
                {
                    throw new FileNotFoundException("Den angivne billedfil blev ikke fundet.", image);
                }

                miscellaneous.Picture = File.ReadAllBytes(image);
            }

            if (miscellaneous.Picture == null || miscellaneous.Picture.Length == 0)
            {
                throw new Exception("Billede er påkrævet.");
            }

            _miscellaneousRepo.Add(miscellaneous);
        }

        public void Update(Miscellaneous miscellaneous, string image)
        {
            if (string.IsNullOrWhiteSpace(miscellaneous.Name))
            {
                throw new Exception("Navn er påkrævet.");
            }

            if (miscellaneous.PurchasePrice <= 0)
            {
                throw new Exception("Indkøbspris skal være større end 0.");
            }

            if (miscellaneous.Amount <= 0)
            {
                throw new Exception("Antal skal være større end 0.");
            }

            if (!string.IsNullOrEmpty(image))
            {
                if (!File.Exists(image))
                {
                    throw new FileNotFoundException("Den angivne billedfil blev ikke fundet.", image);
                }

                miscellaneous.Picture = File.ReadAllBytes(image);
            }

            if (miscellaneous.Picture == null || miscellaneous.Picture.Length == 0)
            {
                throw new Exception("Billede er påkrævet.");
            }

            _miscellaneousRepo.Update(miscellaneous);
        }

        public void Remove(Miscellaneous miscellaneous)
        {
            if (miscellaneous == null)
            {
                throw new Exception("Du skal vælge en vare først for at slette.");
            }

            var existingMiscellaneous = _miscellaneousRepo.GetById(miscellaneous.Id);

            if (existingMiscellaneous == null)
            {
                throw new Exception("Varen blev ikke fundet i databasen.");
            }

            _miscellaneousRepo.Remove(existingMiscellaneous);
        }
    }
}
