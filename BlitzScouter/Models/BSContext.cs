using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlitzScouter.Models
{
    public class BSContext : DbContext
    {
        public DbSet<BSScout> BS_Rounds { get; set; }
        public DbSet<BSTeam> BS_Teams { get; set; }
        public DbSet<BSMatch> BS_Matches { get; set; }

        public BSContext(DbContextOptions<BSContext> options)
            :base(options)
        {

        }
    }
}
