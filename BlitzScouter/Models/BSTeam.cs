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
        public String abilities { get; set; }
        public String performance { get; set; }
        public String downfalls { get; set; }
        public bool star { get; set; }


        // Other Data
        public List<double> checkboxAverages;
        public List<double> counterAverages;
        public List<BSRaw> rounds;
    }

    
}