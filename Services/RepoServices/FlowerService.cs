using FlowerShop.Models;
using FlowerShop.Models.Enums;
using FlowerShop.Persistence;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace FlowerShop.Services.RepoServices
{
    public class FlowerService
    {
        private readonly IRepo<Flower> _flowerRepo;

        public FlowerService(IRepo<Flower> flowerRepo)
        {
            _flowerRepo = flowerRepo;
        }
        public void Add(Flower flower, string image = null)
        {
            if (string.IsNullOrWhiteSpace(flower.Name))
            {
                throw new Exception("Blomsternavn er påkrævet.");
            }
            if (flower.PotSize == FlowerPotSize.None)
            {
                throw new Exception("Blomsterkrukke størrelse er påkrævet.");
            }
            if (flower.PlantSize == FlowerSize.None)
            {
                throw new Exception("Blomst størrelse er påkrævet.");
            }
            if (flower.SalePrice <= 0)
            {
                throw new Exception("Salgspris skal være større end 0.");
            }
            if (flower.PurchasePrice <= 0)
            {
                throw new Exception("Indkøbspris skal være større end 0.");
            }
            if (!string.IsNullOrEmpty(image))
            {
                if (!File.Exists(image))
                {
                    throw new FileNotFoundException("Den angivne billedfil blev ikke fundet.", image);
                }

                flower.Picture = File.ReadAllBytes(image);
            }
            if (flower.Picture == null || flower.Picture.Length == 0)
            {
                throw new Exception("Billede af bilen er påkrævet.");
            }

            _flowerRepo.Add(flower);
        }

        public void Update(Flower flower, string image = null)
        {
            if (string.IsNullOrWhiteSpace(flower.Name))
            {
                throw new Exception("Blomsternavn er påkrævet.");
            }
            if (flower.PotSize == FlowerPotSize.None)
            {
                throw new Exception("Blomsterkrukke størrelse er påkrævet.");
            }
            if (flower.PlantSize == FlowerSize.None)
            {
                throw new Exception("Blomst størrelse er påkrævet.");
            }
            if (flower.SalePrice <= 0)
            {
                throw new Exception("Salgspris skal være større end 0.");
            }
            if (flower.PurchasePrice <= 0)
            {
                throw new Exception("Indkøbspris skal være større end 0.");
            }
            if (!string.IsNullOrEmpty(image))
            {
                if (!File.Exists(image))
                {
                    throw new FileNotFoundException("Den angivne billedfil blev ikke fundet.", image);
                }

                flower.Picture = File.ReadAllBytes(image);
            }
            if (flower.Picture == null || flower.Picture.Length == 0)
            {
                throw new Exception("Billede af bilen er påkrævet.");
            }

            _flowerRepo.Update(flower);
        }

        public void Remove(Flower flower, string image = null)
        {
            if (flower == null)
            {
                throw new Exception("Du skal vælge en blomst først for at slette.");
            }

            var existingFlower = _flowerRepo.GetById(flower.Id);

            if (existingFlower == null)
            {
                throw new Exception("Blomsten blev ikke fundet i databasen.");
            }

            _flowerRepo.Remove(existingFlower);
        }
    }
}
