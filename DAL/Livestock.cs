using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public abstract class Livestock
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int MilkProduced { get; set; }
        public int Offspring { get; set; }

        public abstract string MakeSound();
        public abstract int ProduceMilk();
        public virtual int Reproduce()
        {
            Random random = new Random();
            Offspring = random.Next(1, 5); // Random offspring count
            return Offspring;
        }

    }
}
