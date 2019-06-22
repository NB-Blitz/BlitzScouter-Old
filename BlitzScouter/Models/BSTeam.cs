using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace BlitzScouter.Models
{
    public class BSTeam
    {
        [System.ComponentModel.DataAnnotations.Key]
        public int Id { get; set; }
        public String teamNum { get; set; }
        public String roundData { get; set; }
    }

    public class BSContext : DbContext{
        public BSContext(DbContextOptions<BSContext> options)
            : base(options)
        {
        }

        public DbSet<BlitzScouter.Models.BSTeam> BlitzScoutingData { get; set; }
    }
}