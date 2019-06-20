using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

using OfficeOpenXml;

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
            counter6 = "0";
            counter7 = "0";
		}

        // Key
        [System.ComponentModel.DataAnnotations.Key]
        public int Id { get; set; }

        // Team Data
        public String teamNum { get; set; }
        public String roundNum { get; set; }
        public String color { get; set; }
        public String comments { get; set; }

		// Scouting Data
		[DisplayName("Crossed Line")]
		public bool checkbox1 { get; set; }
        [DisplayName("NULL")]
        public bool checkbox2 { get; set; }
        [DisplayName("Foul")]
		public bool checkbox3 { get; set; }
		[DisplayName("Broken")]
		public bool checkbox4 { get; set; }

        [DisplayName("Cargo in Rocket")]
		public String counter1 { get; set; }
		[DisplayName("Cargo in Cargo Ship")]
		public String counter2 { get; set; }
		[DisplayName("Hatch in Rocket")]
		public String counter3 { get; set; }
		[DisplayName("Hatch in Cargo Ship")]
		public String counter4 { get; set; }
		[DisplayName("Ending Habitat")]
		public String counter5 { get; set; }
        [DisplayName("Pieces Placed")]
        public String counter6{ get; set; }
        [DisplayName("Start Habitat")]
        public String counter7{ get; set; }

        /* Condense Functions into an Array
		public string[] condenseData()
		{
			string[] arr = new string[15];
			
			arr[0] = teamNum;
            arr[1] = roundNum;
			arr[2] = color;

			arr[3] = checkbox1.ToString();
			arr[4] = checkbox2.ToString();
			arr[5] = checkbox3.ToString();
			arr[6] = checkbox4.ToString();

			arr[7] = counter1;
			arr[8] = counter2;
			arr[9] = counter3;
			arr[10] = counter4;
			arr[11] = counter5;
            arr[12] = counter6;
            arr[13] = counter7;

            arr[14] = comments;

			return arr;
		}

        // Condense Titles into an Array
        public string[] condenseTitles()
        {
            string[] arr = new string[15];
            arr[0] = "Team Num";
            arr[1] = "Round Num";
            arr[2] = "Color";

            arr[3] = "Crossed Line";
            arr[4] = "Null";
            arr[5] = "Foul";
            arr[6] = "Broken";
        
            arr[7] = "Cargo in Rocket";
            arr[8] = "Cargo in Cargo Ship";
            arr[9] = "Hatch in Rocket";
            arr[10] = "Hatch in Cargo Ship";
            arr[11] = "End Habitat";
            arr[12] = "Pieces Placed";
            arr[13] = "Start Habitat";

            arr[14] = "Comments";

            return arr;
        }
        */
    }
}