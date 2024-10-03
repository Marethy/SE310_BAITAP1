using DAL;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class FarmManager
    {
        DAL.FarmContext _context;
        public FarmManager()
        {
             _context = new FarmContext();
        }

        public void AddLivestock(Livestock livestock)
        {
            _context.Livestocks.Add(livestock);
            _context.SaveChanges();
        }
        public List<DAL.Livestock> GetAllLiveStock()
        {
            List<DAL.Livestock> liveStocks= _context.Livestocks.ToList();
            return liveStocks;
        }

        public List<DAL.Cow> GetAllCows()
        {
            List<DAL.Cow> cows = _context.Cows.ToList();
            return cows;
        }
        public List<DAL.Goat> GetAllGoats()
        {
            List<DAL.Goat> goats = _context.Livestocks.Where(LivestockType => LivestockType is DAL.Goat).Cast<DAL.Goat>().ToList();
            return goats;
        }
        public List<DAL.Sheep> GetAllSheeps()
        {
            List<DAL.Sheep> sheeps = _context.Livestocks.OfType<DAL.Sheep>().ToList();
            return sheeps;
        }

        public void UpdateLivestock(Livestock livestock)
        {
            _context.Livestocks.Update(livestock);
            _context.SaveChanges();
        }

      

        public void DeleteLivestock(Livestock livestock)
        {
            _context.Livestocks.Remove(livestock);
            _context.SaveChanges();
        }
        public void FeedAnimals()
        {
            foreach (var animal in _context.Livestocks)
            {
                Console.WriteLine(animal.MakeSound());               
            }
        }

        public int CollectMilk()
        {
            int totalMilk = 0;
            foreach (var animal in _context.Livestocks)
            {
                totalMilk += animal.ProduceMilk();
            }
            return totalMilk;
        }

        public int ReproduceAnimals()
        {
            int quantity = 0;
            foreach (var animal in _context.Livestocks)
            {
                 quantity += animal.Reproduce();
            }
            _context.SaveChanges();  // Lưu thay đổi vào cơ sở dữ liệu
            return quantity;
        }




        public void DisplayStatistics()
        {
            int cows = _context.Cows.Count();
            int sheeps = _context.Sheeps.Count();
            int goats = _context.Goats.Count();

            int totalMilk = CollectMilk();

            Console.WriteLine($"Cows: {cows}, Sheeps: {sheeps}, Goats: {goats}");
            Console.WriteLine($"Total Milk Produced: {totalMilk} liters");
        }
    }
}
