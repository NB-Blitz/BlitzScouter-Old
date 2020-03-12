using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace BlitzScouter.Models
{
    public class BSTeam : BSRaw
    {
		public String name { get; set; }
        public bool star { get; set; }
        public String commentsP { get; set; }

        [NotMapped]
        public List<double> checkboxAverages;
        [NotMapped]
        public List<double> counterAverages;
        [NotMapped]
        public List<BSScout> rounds;
    }
}