using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace BlitzScouter.Models
{
    public class DataModel
    {
        // Team Data
        public String teamNum { get; set; }
        public String color { get; set; }

        // Scouting Data
        [DisplayName("Test Check 1")]
        public Boolean checkbox1 { get; set; }

        [DisplayName("Test Count 1")]
        public int counter1 { get; set; }
    }
}
