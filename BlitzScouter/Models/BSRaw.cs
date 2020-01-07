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
        // Other Data
        [System.ComponentModel.DataAnnotations.Key]
        public int id { get; set; }
        public int team { get; set; }
        public int round { get; set; }
        public String color { get; set; }
        public String comments { get; set; }

        // For SQL Use
        public String checkbox { get; set; }
        public String counter { get; set; }

        // For ASP.NET Use
        public bool[] checkboxes;
        public int[] counters;
        

        // SQL --> ASP.NET
        public void toObj()
        {
            if (checkbox == null || counter == null)
                return;

            String[] checkboxSplit = checkbox.Split(',');
            checkboxes = new bool[checkboxSplit.Length];
            for (int i = 0; i < checkboxSplit.Length; i++)
                checkboxes[i] = bool.Parse(checkboxSplit[i]);

            String[] counterSplit = counter.Split(',');
            counters = new int[counterSplit.Length];
            for (int i = 0; i < counterSplit.Length; i++)
                counters[i] = int.Parse(counterSplit[i]);
        }

        // ASP.NET --> SQL
        public void toStr()
        {
            if (checkboxes == null || counters == null)
                return;

            checkbox = "";
            counter = "";
            
            for (int i = 0; i < checkboxes.Length; i++)
            {
                checkbox += checkboxes[i].ToString();
                if (i + 1 != checkboxes.Length)
                    checkbox += ",";
            }
            System.Diagnostics.Debug.WriteLine(checkbox);

            for (int i = 0; i < counters.Length; i++)
            {
                counter += counters[i].ToString();
                if (i + 1 != counters.Length)
                    counter += ",";
            }
            System.Diagnostics.Debug.WriteLine(counter);
        }
    }
}