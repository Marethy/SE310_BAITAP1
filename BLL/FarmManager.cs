using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class FarmManager
    {
        private readonly FarmContext _context;
        public FarmManager(FarmContext context)
        {
            _context = context;
        }

        public void AddLivestock(Livestock livestock)
        {
            _context.Livestocks.Add(livestock);
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
