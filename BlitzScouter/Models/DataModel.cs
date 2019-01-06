using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace BlitzScouter.Models
{
    public class DataModel
    {
        // Default Values
        public DataModel()
        {
            counter1 = 0.ToString();
            counter2 = 0.ToString();
            counter3 = 0.ToString();
            counter4 = 0.ToString();
            counter5 = 0.ToString();
        }

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
        [DisplayName("Crossed Line")]
        public bool checkbox1 { get; set; }
        [DisplayName("Placed Piece")]
        public bool checkbox2 { get; set; }
        [DisplayName("Foaled")]
        public bool checkbox3 { get; set; }
        [DisplayName("Broke")]
        public bool checkbox4 { get; set; }

        [DisplayName("Fuel in Rocket")]
        public String counter1 { get; set; }
        [DisplayName("Fuel in Cargo Ship")]
        public String counter2 { get; set; }
        [DisplayName("Hatch in Rocket")]
        public String counter3 { get; set; }
        [DisplayName("Hatch in Cargo Ship")]
        public String counter4 { get; set; }
        [DisplayName("Habitat Level")]
        public String counter5 { get; set; }
    }
}
