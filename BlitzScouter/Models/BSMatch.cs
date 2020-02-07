using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlitzScouter.Models
{
    public class BSMatch
    {
        public String match { get; set; }
        public String matchStr { get; set; }
        public List<BSTeam> blue { get; set; }
        public List<BSTeam> red { get; set; }
    }
}
