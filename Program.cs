using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgentsManager
{
    class Program
    {
        static void Main(string[] args)
        {
            AgentDAL dal = new AgentDAL();
            bool exit = false;

            while (!exit)
            {
                Console.Clear();
                Console.WriteLine("==== Agent Manager ====");
                Console.WriteLine("1. Add Agent");
                Console.WriteLine("2. Show All Agents");
                Console.WriteLine("3. Show Agent by ID");
                Console.WriteLine("4. Update Agent");
                Console.WriteLine("5. Delete Agent");
                Console.WriteLine("6. Exit");
                Console.Write("Choose an option: ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        AddAgent(dal);
                        break;
                    case "2":
                        ShowAllAgents(dal);
                        break;
                    case "3":
                        ShowAgentById(dal);
                        break;
                    case "4":
                        UpdateAgent(dal);
                        break;
                    case "5":
                        DeleteAgent(dal);
                        break;
                    case "6":
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Invalid option. Press any key to try again...");
                        Console.ReadKey();
                        break;
                }
            }
        }

        static void AddAgent(AgentDAL dal)
        {
            Console.Clear();
            Console.WriteLine("--- Add New Agent ---");

            Console.Write("Code Name: ");
            string codeName = Console.ReadLine();
            Console.Write("Real Name: ");
            string realName = Console.ReadLine();
            Console.Write("Location: ");
            string location = Console.ReadLine();
            string status = GetValidStatusFromUser();
            Console.Write("Missions Completed: ");
            int missionsCompleted = int.Parse(Console.ReadLine());

            Agent agent = new Agent(0, codeName, realName, location, status, missionsCompleted);
            dal.AddAgent(agent);


            Console.WriteLine("Agent added successfully. Press any key to continue...");
            Console.ReadKey();
        }

        static void ShowAllAgents(AgentDAL dal)
        {
            Console.Clear();
            Console.WriteLine("--- All Agents ---");
            var agents = dal.GetAgents();
            foreach (var agent in agents)
            {
                PrintAgent(agent);
                Console.WriteLine("-------------------------");
            }
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }

        static void ShowAgentById(AgentDAL dal)
        {
            Console.Clear();
            Console.Write("Enter Agent ID: ");
            int id = int.Parse(Console.ReadLine());

            var agent = dal.GetAgentById(id);
            if (agent != null)
            {
                PrintAgent(agent);
            }
            else
            {
                Console.WriteLine("Agent not found.");
            }

            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }

        static void UpdateAgent(AgentDAL dal)
        {
            Console.Clear();
            Console.WriteLine("--- Update Agent ---");
            Console.Write("Enter Agent ID: ");
            int id = int.Parse(Console.ReadLine());

            var agent = dal.GetAgentById(id);
            if (agent == null)
            {
                Console.WriteLine("Agent not found.");
                Console.ReadKey();
                return;
            }

            Console.Write($"Code Name ({agent.CodeName}): ");
            agent.CodeName = Console.ReadLine();

            Console.Write($"Real Name ({agent.RealName}): ");
            agent.RealName = Console.ReadLine();

            Console.Write($"Location ({agent.Location}): ");
            agent.Location = Console.ReadLine();

            agent.Status = GetValidStatusFromUser($"Status ({agent.Status}) (Active, Injured, Missing, Retired): ");

            Console.Write($"Missions Completed ({agent.MissionsCompleted}): ");
            agent.MissionsCompleted = int.Parse(Console.ReadLine());

            dal.UpdateAgent(agent);
            Console.WriteLine("Agent updated successfully.");
            Console.ReadKey();
        }

        static void DeleteAgent(AgentDAL dal)
        {
            Console.Clear();
            Console.Write("Enter Agent ID to delete: ");
            int id = int.Parse(Console.ReadLine());

            dal.DeleteAgent(id);
            Console.WriteLine("Agent deleted successfully.");
            Console.ReadKey();
        }

        static void PrintAgent(Agent agent)
        {
            Console.WriteLine($"ID: {agent.Id}");
            Console.WriteLine($"Code Name: {agent.CodeName}");
            Console.WriteLine($"Real Name: {agent.RealName}");
            Console.WriteLine($"Location: {agent.Location}");
            Console.WriteLine($"Status: {agent.Status}");
            Console.WriteLine($"Missions Completed: {agent.MissionsCompleted}");
        }
        static string GetValidStatusFromUser(string message = "Status (Active, Injured, Missing, Retired): ")
        {
            string[] validStatuses = { "Active", "Injured", "Missing", "Retired" };

            while (true)
            {
                Console.Write(message);
                string input = Console.ReadLine().Trim();

                if (validStatuses.Contains(input, StringComparer.OrdinalIgnoreCase))
                {
                    // מחזירים את הערך כמו שהוא כתוב ברשימה המקורית (עם אות ראשונה גדולה)
                    return validStatuses.First(s => s.Equals(input, StringComparison.OrdinalIgnoreCase));
                }

                Console.WriteLine("Invalid status. Please enter one of: Active, Injured, Missing, Retired.");
            }
        }

    }
}