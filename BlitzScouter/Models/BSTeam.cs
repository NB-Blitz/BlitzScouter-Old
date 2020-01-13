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
        // SQL data
        [System.ComponentModel.DataAnnotations.Key]
        public int id { get; set; }
        public int team { get; set; }
		public String name { get; set; }
        public String pitComments { get; set; }

        // Other Data
        public List<double> averages;
        public List<BSRaw> rounds;
    }

    
}