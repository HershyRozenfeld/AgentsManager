using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgentsManager
{
    internal class Agent
    {
        public int Id { get; set; }
        public string CodeName { get; set; }
        public string RealName { get; set; }
        public string Location { get; set; }
        public string Status { get; set; }
        public int MissionsCompleted { get; set; }

        public Agent(int id, string codeName, string realName, string location, string status, int missionsCompleted)
        {
            Id = id;
            CodeName = codeName;
            RealName = realName;
            Location = location;
            Status = status;
            MissionsCompleted = missionsCompleted;
        }
        public Agent()
        {

        }
    }
}
