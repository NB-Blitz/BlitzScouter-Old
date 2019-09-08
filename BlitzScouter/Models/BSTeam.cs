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
        [System.ComponentModel.DataAnnotations.Key]
        public int id { get; set; }
        public String team { get; set; }
		public String name { get; set; }
        public String pitComments { get; set; }
    }

    
}