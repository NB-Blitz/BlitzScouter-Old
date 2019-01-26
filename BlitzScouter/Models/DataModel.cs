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
            counter1 = "0";
            counter2 = "0";
            counter3 = "0";
            counter4 = "0";
            counter5 = "0";
        }

        // Data Management Methods
        public bool uploadData()
        {
            return false;
        }

        private String[] condenseData()
        {
            string[] arr = new string[11];
            
            arr[0] = teamNum;
            arr[1] = color;

            arr[2] = checkbox1.ToString();
            arr[3] = checkbox2.ToString();
            arr[4] = checkbox3.ToString();
            arr[5] = checkbox4.ToString();

            arr[6] = counter1;
            arr[7] = counter2;
            arr[8] = counter3;
            arr[9] = counter4;
            arr[10] = counter5;

            return arr;
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