using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlitzScouter.Models
{
    public class BSMatch
    {
        public int match { get; set; }
        public List<BSTeam> blue { get; set; }
        public List<BSTeam> red { get; set; }
    }
}
