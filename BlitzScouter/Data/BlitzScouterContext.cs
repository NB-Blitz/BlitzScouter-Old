using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace BlitzScouter.Models
{
    public class BlitzScouterContext : DbContext
    {
        public BlitzScouterContext (DbContextOptions<BlitzScouterContext> options)
            : base(options)
        {
        }

        public DbSet<BlitzScouter.Models.DataModel> DataModel { get; set; }
    }
}
