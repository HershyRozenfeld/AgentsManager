using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Google.Protobuf.Reflection.SourceCodeInfo.Types;


namespace AgentsManager
{
    internal class AgentDAL
    {
        private readonly string _connStr = "server=localhost;user=root;password=;database=agentmanager";

        public List<Agent> GetAgents()
        {
            var agents = new List<Agent>();
            using (var conn = new MySqlConnection(_connStr))
            {
                try
                {
                    conn.Open();
                    var query = "SELECT * FROM agent";
                    using (var cmd = new MySqlCommand(query, conn))
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            agents.Add(new Agent(
                                reader.GetInt32("id"),
                                reader.GetString("codeName"),
                                reader.GetString("realName"),
                                reader.GetString("location"),
                                reader.GetString("status"),
                                reader.GetInt32("missionsCompleted")
                            ));
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Error fetching agents: " + e.Message);
                }
            }

            return agents;
        }

        public Agent GetAgentById(int id)
        {
            Agent agent = null;
            using (var conn = new MySqlConnection(_connStr))
            {
                try
                {
                    conn.Open();
                    var query = "SELECT * FROM agent WHERE id = @id";
                    using (var cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@id", id);
                        using (var reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                agent = new Agent(
                                    reader.GetInt32("id"),
                                    reader.GetString("codeName"),
                                    reader.GetString("realName"),
                                    reader.GetString("location"),
                                    reader.GetString("status"),
                                    reader.GetInt32("missionsCompleted")
                                );
                            }
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Error fetching agent by ID: " + e.Message);
                }
            }

            return agent;
        }

        public void AddAgent(Agent agent)
        {
            using (var conn = new MySqlConnection(_connStr))
            {
                try
                {
                    conn.Open();
                    var query = @"INSERT INTO agent 
                        (codeName, realName, location, status, missionsCompleted)
                        VALUES (@codeName, @realName, @location, @status, @missionsCompleted)";

                    using (var cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@codeName", agent.CodeName);
                        cmd.Parameters.AddWithValue("@realName", agent.RealName);
                        cmd.Parameters.AddWithValue("@location", agent.Location);
                        cmd.Parameters.AddWithValue("@status", agent.Status);
                        cmd.Parameters.AddWithValue("@missionsCompleted", agent.MissionsCompleted);
                        cmd.ExecuteNonQuery();
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Error adding agent: " + e.Message);
                }
            }
        }

        public void UpdateAgent(Agent agent)
        {
            using (var conn = new MySqlConnection(_connStr))
            {
                try
                {
                    conn.Open();
                    var query = @"UPDATE agent 
                                  SET codeName = @codeName, realName = @realName, location = @location, 
                                      status = @status, missionsCompleted = @missionsCompleted
                                  WHERE id = @id";

                    using (var cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@codeName", agent.CodeName);
                        cmd.Parameters.AddWithValue("@realName", agent.RealName);
                        cmd.Parameters.AddWithValue("@location", agent.Location);
                        cmd.Parameters.AddWithValue("@status", agent.Status);
                        cmd.Parameters.AddWithValue("@missionsCompleted", agent.MissionsCompleted);
                        cmd.Parameters.AddWithValue("@id", agent.Id);

                        int rowsAffected = cmd.ExecuteNonQuery();
                        if (rowsAffected == 0)
                        {
                            Console.WriteLine($"No agent with ID {agent.Id} found to update.");
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Error updating agent: " + e.Message);
                }
            }
        }

        public void DeleteAgent(int id)
        {
            using (var conn = new MySqlConnection(_connStr))
            {
                try
                {
                    conn.Open();
                    var query = "DELETE FROM agent WHERE id = @id";
                    using (var cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@id", id);
                        int rowsAffected = cmd.ExecuteNonQuery();
                        if (rowsAffected == 0)
                        {
                            Console.WriteLine($"No agent with ID {id} found to delete.");
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Error deleting agent: " + e.Message);
                }
            }
        }
    }
}
