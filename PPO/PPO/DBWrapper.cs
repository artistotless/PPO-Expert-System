using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace PPO
{
    class DBWrapper : DbContext
    {
        public DbSet<Graph> Graphs { get; set; }
        public DbSet<Transition> Transitions { get; set; }
        public DbSet<Smartphone> Smartphones { get; set; }

        public DBWrapper() : base("DefaultConnection")
        {
        }
    }
}
