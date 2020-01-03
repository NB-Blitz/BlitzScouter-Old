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
        // Team Data
        [System.ComponentModel.DataAnnotations.Key]
        public int id { get; set; }
        public int team { get; set; }
        public int round { get; set; }
        public String color { get; set; }
        public String comments { get; set; }

        public bool[] checkboxes;
        public int[] counters;
    }
}