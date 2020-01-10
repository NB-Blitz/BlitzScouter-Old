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
        /*
         *       CONFIGURATION FILE LOCATION
         *      -----------------------------
         *         Default file location is
         *              ./config.txt
         *      ----------------------------- 
         */

        private const String CONFIG_FILE_LOCATION = "./config.txt";


        public static ArrayList components = new ArrayList();
        public static String tbaKey = "";
        public static String tbaComp = "";
        public static int checkboxCounter = 0;
        public static int counterCounter = 0;
        public static bool initialized = false;

        public static void initialize()
        {
            if (initialized)
                return;
            String[] output = File.ReadAllLines(CONFIG_FILE_LOCATION);
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
                    case "desc":
                        components.Add(new Description(split));
                        break;
                    case "counter":
                        components.Add(new Counter(split));
                        counterCounter++;
                        break;
                    case "tba-api-key":
                        tbaKey = split[1];
                        break;
                    case "tba-comp":
                        tbaComp = split[1];
                        break;
                    default:
                        System.Diagnostics.Debug.WriteLine("CONFIG PARSE ERROR: Unknown type '" + split[0] + "'.");
                        break;
                }
            }
            initialized = true;
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

    public class Description : Type
    {
        public Description(String[] data)
        {
            if (data.Length < 2)
            {
                System.Diagnostics.Debug.WriteLine("CONFIG PARSE ERROR: Not enough arguments '" + data.Length + "'.");
                return;
            }

            this.type = data[0];
            this.title = data[1];
            for (int i = 2; i < data.Length; i++)
                this.title += "," + data[i];
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
