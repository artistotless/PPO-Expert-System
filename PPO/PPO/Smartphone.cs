using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPO
{
    public class Smartphone
    {
        public int id { get; set; }
        public string name { get; set; }
        public int cost { get; set; }
        public string descr { get; set; }
        public string image { get; set; }
        public string link { get; set; }

        public Smartphone()
        {
        }

        public Smartphone(int id, string name, int cost, string descr, string image, string link)
        {
            this.id = id;
            this.name = name;
            this.cost = cost;
            this.descr = descr;
            this.image = image;
            this.link = link;
        }
    }
}
