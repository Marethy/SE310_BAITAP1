using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class Sheep: Livestock
    {
        public override string MakeSound() => "Baaaa!";
        public override int ProduceMilk()
        {
            Random random= new Random();
            return random.Next(0, 6);
        }
    }
}
