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
         *              ./config.txt
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
    }
    public class YAMLRoot
    {
        public string tbaApiKey { get; set; }
        public string tbaComp { get; set; }
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
