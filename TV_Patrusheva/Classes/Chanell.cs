using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TV_Patrusheva.Classes
{
    public class Chanell
    {
       public string Name { get; set; }

       public List<string> peredacha {  get; set; } = new List<string>(); // список передач для каналов
        public int memoryPeredacha { get; set; } = 0; // чтоб помнить на какой передаче остановились
    }
}
