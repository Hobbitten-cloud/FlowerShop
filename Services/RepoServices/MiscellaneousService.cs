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
                throw new Exception("Blomsternavn er påkrævet.");
            }

            //if (miscellaneous.PotSize == FlowerPotSize.None)
            //{
            //    throw new Exception("Blomsterkrukke størrelse er påkrævet.");
            //}

            //if (miscellaneous.PlantSize == FlowerSize.None)
            //{
            //    throw new Exception("Blomst størrelse er påkrævet.");
            //}

            //if (miscellaneous.SalePrice <= 0)
            //{
            //    throw new Exception("Salgspris skal være større end 0.");
            //}

            if (miscellaneous.PurchasePrice <= 0)
            {
                throw new Exception("Indkøbspris skal være større end 0.");
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
                throw new Exception("Billede af bilen er påkrævet.");
            }

            _miscellaneousRepo.Add(miscellaneous);
        }

        public void Update(Miscellaneous miscellaneous, string image)
        {
            if (string.IsNullOrWhiteSpace(miscellaneous.Name))
            {
                throw new Exception("Blomsternavn er påkrævet.");
            }

            //if (miscellaneous.PotSize == FlowerPotSize.None)
            //{
            //    throw new Exception("Blomsterkrukke størrelse er påkrævet.");
            //}

            //if (miscellaneous.PlantSize == FlowerSize.None)
            //{
            //    throw new Exception("Blomst størrelse er påkrævet.");
            //}

            //if (miscellaneous.SalePrice <= 0)
            //{
            //    throw new Exception("Salgspris skal være større end 0.");
            //}

            if (miscellaneous.PurchasePrice <= 0)
            {
                throw new Exception("Indkøbspris skal være større end 0.");
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
                throw new Exception("Billede af bilen er påkrævet.");
            }

            _miscellaneousRepo.Update(miscellaneous);
        }

        public void Remove(Miscellaneous miscellaneous)
        {
            if (miscellaneous == null)
            {
                throw new Exception("Du skal vælge en ting først for at slette.");
            }

            var existingMiscellaneous = _miscellaneousRepo.GetById(miscellaneous.Id);

            if (existingMiscellaneous == null)
            {
                throw new Exception("Tingen blev ikke fundet i databasen.");
            }

            _miscellaneousRepo.Remove(existingMiscellaneous);
        }
    }
}
