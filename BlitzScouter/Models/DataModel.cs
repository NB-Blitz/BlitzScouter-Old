using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace BlitzScouter.Models
{
    public class DataModel
    {
        // Data Management Methods
        public bool connectToServer()
        {
            return false;
        }

        public bool uploadData()
        {
            return false;
        }

        private String[] condenseData()
        {
            return null;
        }

        // Team Data
        public String teamNum { get; set; }
        public String color { get; set; }

        // Scouting Data
        [DisplayName("Test Check 1")]
        public bool checkbox1 { get; set; }
        [DisplayName("Test Check 2")]
        public bool checkbox2 { get; set; }
        [DisplayName("Test Check 3")]
        public bool checkbox3 { get; set; }

        [DisplayName("Test Count 1")]
        public int counter1 { get; set; }
    }
}
