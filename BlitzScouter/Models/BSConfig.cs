using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BlitzScouter.Models
{
    public class BSConfig
    {
        public ArrayList components = new ArrayList();
        public int checkboxCounter = 0;
        public int counterCounter = 0;

        public BSConfig(String filePath)
        {
            String[] output = File.ReadAllLines(filePath);
            foreach (String line in output)
            {
                String[] split = line.Split(',');
                if (split.Length < 2)
                    return;

                switch (split[0])
                {
                    case "checkbox":
                        components.Add(new Checkbox(split));
                        checkboxCounter++;
                        break;
                    case "header":
                        components.Add(new Header(split));
                        break;
                    case "counter":
                        components.Add(new Counter(split));
                        counterCounter++;
                        break;
                    default:
                        System.Diagnostics.Debug.WriteLine("CONFIG PARSE ERROR: Unknown type '" + split[0] + "'.");
                        break;
                }
            }
        }
    }
    
    /*
     *      Object Types
     */

    public abstract class Type
    {
        public String title = "";
        public String type = "";
    }

    public class Checkbox : Type
    {
        public bool data = false;
        public Checkbox(String[] data)
        {
            if (data.Length < 3) {
                System.Diagnostics.Debug.WriteLine("CONFIG PARSE ERROR: Not enough arguments '" + data.Length + "'.");
                return;
            }

            this.type = data[0];
            this.title = data[1];
            this.data = bool.Parse(data[2]);
        }
    }

    public class Header : Type
    {
        public Header(String[] data)
        {
            if (data.Length < 2)
            {
                System.Diagnostics.Debug.WriteLine("CONFIG PARSE ERROR: Not enough arguments '" + data.Length + "'.");
                return;
            }

            this.type = data[0];
            this.title = data[1];
        }
    }

    public class Counter : Type
    {
        public int min = 0;
        public int max = 10;

        public Counter(String[] data)
        {
            if (data.Length < 4)
            {
                System.Diagnostics.Debug.WriteLine("CONFIG PARSE ERROR: Not enough arguments '" + data.Length + "'.");
                return;
            }

            this.type = data[0];
            this.title = data[1];
            this.min = int.Parse(data[2]);
            this.max = int.Parse(data[3]);
        }
    }
}
