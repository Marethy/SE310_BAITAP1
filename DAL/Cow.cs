using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class Cow : Livestock
    {
        public override string MakeSound() => "Moo!";
        public override int ProduceMilk()
        {
            Random random = new Random();
            return random.Next(0, 21); // 0-20 liters of milk
        }

    }
}
