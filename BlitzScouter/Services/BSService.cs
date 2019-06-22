using BlitzScouter.Models;
using BlitzScouter.Repository;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace BlitzScouter.Services
{
    public class BSService
    {
        BSRepo repo;

        public BSService(BSContext context)
        {
            repo = new BSRepo(context);
        }

        public void addUserData(BSRaw model)
        {
            if (repo.containsTeam(model.teamNum))
            {
                BSTeam team = repo.getTeam(model.teamNum);
                string roundData = team.roundData;

                roundData += "," + serialize(model);

                team.roundData = roundData;
                repo.saveData();
            }
            else
            {
                BSTeam team = new BSTeam();
                team.teamNum = model.teamNum;
                team.roundData = serialize(model);
                repo.addData(team);
            }
        }

        private string serialize(BSRaw data)
        {
            StringWriter writer = new StringWriter(CultureInfo.InvariantCulture);
            XmlSerializer serializer = new XmlSerializer(typeof(BSRaw));
            serializer.Serialize(writer, data);
            return writer.ToString();
        }
    }
}
