using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BlitzScouter.Models
{
    public class BSMatch
    {
        [System.ComponentModel.DataAnnotations.Key]
        public int id { get; set; }
        public String match { get; set; }
        public String comments { get; set; }

        [NotMapped]
        public String matchStr { get; set; }
        [NotMapped]
        public List<BSTeam> blue { get; set; }
        [NotMapped]
        public List<BSTeam> red { get; set; }
    }
}
