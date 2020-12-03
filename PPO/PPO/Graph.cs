using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPO
{
    public class Graph
    {
        public int id { get; set; }
        public string question { get; set; }
        public int result { get; set; }
        public string explanations { get; set; }

        public Graph()
        {
        }

        public Graph(int id, string question, int result, string explanations)
        {
            this.id = id;
            this.question = question;
            this.result = result;
            this.explanations = explanations;
        }
    }
}
