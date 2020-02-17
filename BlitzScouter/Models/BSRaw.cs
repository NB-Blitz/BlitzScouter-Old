using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace BlitzScouter.Models
{
	public class BSRaw
	{
        // Other Data
        [System.ComponentModel.DataAnnotations.Key]
        public int id { get; set; }
        public int team { get; set; }
        public int round { get; set; }
        public String comments { get; set; }

        // For SQL Use (Serialized)
        public String checkbox { get; set; }
        public String counter { get; set; }

        // For ASP.NET Use (Deserialized)
        [NotMapped]
        public List<bool> checkboxes { get; set; }
        [NotMapped]
        public List<int> counters { get; set; }
        

        // SQL --> ASP.NET (Deserializer)
        public void toObj()
        {
            if (checkbox == null || counter == null)
                return;

            String[] checkboxSplit = checkbox.Split(',');
            checkboxes = new List<bool>();
            for (int i = 0; i < checkboxSplit.Length; i++)
                checkboxes.Add(bool.Parse(checkboxSplit[i]));

            String[] counterSplit = counter.Split(',');
            counters = new List<int>();
            for (int i = 0; i < counterSplit.Length; i++)
                counters.Add(int.Parse(counterSplit[i]));
        }

        // ASP.NET --> SQL (Serializer)
        public void toStr()
        {
            if (checkboxes == null || counters == null)
                return;

            checkbox = "";
            counter = "";
            
            for (int i = 0; i < checkboxes.Count; i++)
            {
                checkbox += checkboxes[i].ToString();
                if (i + 1 != checkboxes.Count)
                    checkbox += ",";
            }
            System.Diagnostics.Debug.WriteLine(checkbox);

            for (int i = 0; i < counters.Count; i++)
            {
                counter += counters[i].ToString();
                if (i + 1 != counters.Count)
                    counter += ",";
            }
            System.Diagnostics.Debug.WriteLine(counter);
        }
    }
}