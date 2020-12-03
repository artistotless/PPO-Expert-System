using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPO
{
    public class Transition
    {
        public int id { get; set; }
        public string text { get; set; }
        public int initialItemIndex { get; set; }
        public int nextItemIndex { get; set; }

        public Transition()
        {
        }

        public Transition(int id, int initialItemIndex, string text, int nextItemIndex)
        {
            this.id = id;
            this.text = text;
            this.initialItemIndex = initialItemIndex;
            this.nextItemIndex = nextItemIndex;
        }
    }
}
