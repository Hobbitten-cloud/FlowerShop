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
    public class WreathService
    {
        private readonly IRepo<Wreath> _wreathRepo;

        public WreathService(IRepo<Wreath> wreathRepo)
        {
            _wreathRepo = wreathRepo;
        }

        public void Add(Wreath wreath, string image)
        {
            if (string.IsNullOrWhiteSpace(wreath.Name))
            {
                throw new Exception("Navn er påkrævet.");
            }

            if (wreath.Size == WreathSize.Size27_1)
            {
                throw new Exception("Størrelse er påkrævet.");
            }

            if (wreath.SalePrice <= 0)
            {
                throw new Exception("Salgspris skal være større end 0.");
            }

            if (wreath.PurchasePrice <= 0)
            {
                throw new Exception("Indkøbspris skal være større end 0.");
            }

            if (wreath.Amount <= 0)
            {
                throw new Exception("Antal skal være større end 0.");
            }

            if (!string.IsNullOrEmpty(image))
            {
                if (!File.Exists(image))
                {
                    throw new FileNotFoundException("Den angivne billedfil blev ikke fundet.", image);
                }

                wreath.picture = File.ReadAllBytes(image);
            }

            if (wreath.picture == null || wreath.picture.Length == 0)
            {
                throw new Exception("Billede er påkrævet.");
            }

            _wreathRepo.Add(wreath);
        }

        public void Update(Wreath wreath, string image)
        {
            if (string.IsNullOrWhiteSpace(wreath.Name))
            {
                throw new Exception("Navn er påkrævet.");
            }

            if (wreath.Size == WreathSize.Size27_1)
            {
                throw new Exception("Størrelse er påkrævet.");
            }

            if (wreath.SalePrice <= 0)
            {
                throw new Exception("Salgspris skal være større end 0.");
            }

            if (wreath.PurchasePrice <= 0)
            {
                throw new Exception("Indkøbspris skal være større end 0.");
            }

            if (wreath.Amount <= 0)
            {
                throw new Exception("Antal skal være større end 0.");
            }

            if (!string.IsNullOrEmpty(image))
            {
                if (!File.Exists(image))
                {
                    throw new FileNotFoundException("Den angivne billedfil blev ikke fundet.", image);
                }

                wreath.picture = File.ReadAllBytes(image);
            }

            if (wreath.picture == null || wreath.picture.Length == 0)
            {
                throw new Exception("Billede er påkrævet.");
            }

            _wreathRepo.Update(wreath);
        }

        public void Remove(Wreath wreath)
        {
            if (wreath == null)
            {
                throw new Exception("Du skal vælge en krans først for at slette.");
            }

            var existingWreath = _wreathRepo.GetById(wreath.Id);

            if (existingWreath == null)
            {
                throw new Exception("Kransen blev ikke fundet i databasen.");
            }

            _wreathRepo.Remove(existingWreath);
        }
    }
} 