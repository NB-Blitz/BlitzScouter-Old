using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace BlitzScouter.Models
{
	public class BSRaw
	{
		// Default Values
		public BSRaw()
		{
			counter1 = "0";
			counter2 = "0";
			counter3 = "0";
			counter4 = "0";
			counter5 = "0";
            counter6 = "0";
            counter7 = "0";
		}

        // Team Data
        [System.ComponentModel.DataAnnotations.Key]
        public int id { get; set; }
        public String team { get; set; }
        public int round { get; set; }
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
    }
}