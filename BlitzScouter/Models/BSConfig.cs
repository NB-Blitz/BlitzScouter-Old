using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using YamlDotNet.Serialization;

namespace BlitzScouter.Models
{
    public class BSConfig
    {
        /*
         *       CONFIGURATION FILE LOCATION
         *      -----------------------------
         *         Default file location is
         *              ./config.yml
         *      ----------------------------- 
         */

        private const String CONFIG_FILE_LOCATION = "./config.yml";
        public static YAMLRoot c;
        public static bool initialized = false;

        public static void initialize()
        {
            if (initialized)
                return;
            String output = File.ReadAllText(CONFIG_FILE_LOCATION);
            var deserializer = new DeserializerBuilder().Build();
            c = deserializer.Deserialize<YAMLRoot>(output);
            initialized = true;
        }

        public static List<Component> getByType(string type)
        {
            initialize();
            List<Component> comps = new List<Component>();
            for (int i = 0; i < c.matchScout.Count; i++)
            {
                if (c.matchScout[i].type == type)
                {
                    comps.Add(c.matchScout[i]);
                }
            }
            return comps;
        }
    }
    public class YAMLRoot
    {
        public string tbaApiKey { get; set; }
        public string tbaComp { get; set; }
        public int teamnum { get; set; }
        public List<Component> matchScout { get; set; }
    }

    public class Component
    {
        public string type { get; set; }
        public string text { get; set; }
        public int def { get; set; }
        public int min { get; set; }
        public int max { get; set; }
    }
}
