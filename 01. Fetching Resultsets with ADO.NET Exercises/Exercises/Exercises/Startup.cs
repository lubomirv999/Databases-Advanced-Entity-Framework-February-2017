using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exercises
{
    class Program
    {
        static void Main(string[] args)
        {
            SqlConnection connection = new SqlConnection
                (@"Server=(localdb)\MSSQLLocalDB;Database=MinionsDB;Integrated Security=true");

            connection.Open();

            Console.WriteLine("All actions: create, tables, villains, minionNames, addMinion, changeTown, deleteVillain, allMinions, increaseMinionsAge, increaseMinionsAgeProc");
            Console.Write("Enter action: ");
            string action = Console.ReadLine();
            switch (action)
            {
                case "create":
                    CreateDatabase(connection);
                    break;
                case "tables":
                    CreateTables(connection);
                    break;
                case "villains":
                    VillainsNames(connection);
                    break;
                case "minionNames":
                    MinionNames(connection);
                    break;
                case "addMinion":
                    AddMinion(connection);
                    break;
                case "changeTown":
                    ChangeTownName(connection);
                    break;
                case "deleteVillain":
                    DeleteVillain(connection);
                    break;
                case "allMinions":
                    AllMinionNames(connection);
                    break;
                case "increaseMinionsAge":
                    IncreaseMinionsAge(connection);
                    break;
                case "increaseMinionsAgeProc":
                    IncreaseAgeProc(connection);
                    break;
            }     
        }

        //-------------------------------------------------------
        private static void CreateDatabase(SqlConnection connection)
        {
            string query = @"CREATE DATABASE MinionsDB";
            SqlCommand command = new SqlCommand(query, connection);
            command.ExecuteNonQuery();
        }

        //-------------------------------------------------------
        private static void CreateTables(SqlConnection connection)
        {
            string query = File.ReadAllText(@"../../MinionsDB.sql");
            SqlCommand createTablesCommand = new SqlCommand(query, connection);
            Console.WriteLine(createTablesCommand.ExecuteNonQuery());
        }

        //-------------------------------------------------------
        private static void VillainsNames(SqlConnection connection)
        {
            using (connection)
            {
                string query = File.ReadAllText(@"../../VillainNames.sql");

                SqlCommand command = new SqlCommand(query, connection);
                SqlDataReader minionsCount = command.ExecuteReader();

                using (minionsCount)
                {
                    while (minionsCount.Read())
                    {
                        Console.WriteLine("{0} {1}", minionsCount[0], minionsCount[1]);
                    }
                }
            }
        }

        //-------------------------------------------------------
        private static void MinionNames(SqlConnection connection)
        {
            using (connection)
            {
                Console.Write("VillainId: ");
                string villainId = Console.ReadLine();

                using (connection)
                {
                    Villain(connection, villainId);
                    VillainMinions(connection, villainId);
                }
            }         
        }

        private static void Villain(SqlConnection connection, string villain)
        {
            using (connection)
            {
                string query = @"SELECT Name FROM Villains WHERE Id=@villainId";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@villainId", villain);
                SqlDataReader minionsCount = command.ExecuteReader();
                if (minionsCount.Read())
                {
                    Console.WriteLine("Villain: {0}", minionsCount[0]);
                    minionsCount.Close();
                }
                else
                {
                    Console.WriteLine("No villain with ID 10 exists in the database.");
                    minionsCount.Close();
                }
            } 
        }

        private static void VillainMinions(SqlConnection connection, string villainId)
        {
            string query = @"SELECT m.Id, m.Name, m.Age FROM Villains AS v 
JOIN MinionsVillains AS mv ON mv.VillainId = v.Id 
JOIN Minions AS m ON m.Id = mv.MinionId 
WHERE v.Id=@villainId";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@villainId", villainId);
            SqlDataReader reader = command.ExecuteReader();

            using (reader)
            {
                while (reader.Read())
                {
                    Console.WriteLine("{0}. {1} {2}", reader[0], reader[1], reader[2]);
                }
            }
        }

        //-------------------------------------------------------
        private static void AddMinion(SqlConnection connection)
        {
            Console.Write("Minion: ");
            string minion = Console.ReadLine();
            var inputMinion = minion.Split(' ');
            Console.Write("Vallain: ");
            string villain = Console.ReadLine();

            using (connection)
            {
                insertTown(connection, inputMinion);
                int townID = townCheck(connection, inputMinion);
                inserMinion(connection, inputMinion, townID);
                int minionId = minionCheck(connection, inputMinion);
                insertVillain(connection, villain);
                int villainId = villainCheck(connection, villain);
                SetMinionToServe(connection, minionId, villainId, inputMinion[0], villain);
            }
        }

        private static void SetMinionToServe(SqlConnection connection, int minionId, int villainId, string minion, string villain)
        {
            string query = "INSERT INTO MinionsVillains VALUES(@minion, @vallain)";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@minion", minionId);
            command.Parameters.AddWithValue("@vallain", villainId);
            int result = command.ExecuteNonQuery();

            if (result > 0)
            {
                Console.WriteLine($"Successfully added {minion} to be minion of {villain}");
            }
        }

        private static void insertVillain(SqlConnection connection, string villain)
        {
            string villainName = villain;
            string query = "INSERT INTO Villains (Name, EvilnesFactor)";
            query += "VALUES (@villainName, 'evil')";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@villainName", villainName);
            var result = command.ExecuteNonQuery();

            if (result > 0)
            {
                Console.WriteLine($"Villain {villainName} was added to the database.");
            }
            else
            {
                Console.WriteLine("Villain already exist!");
            }
        }

        private static void insertTown(SqlConnection connection, string[] inputMinion)
        {
            string town = inputMinion[2];
            string query = "INSERT INTO Towns (Name, CountryId)";
            query += "VALUES (@Town, 9999)";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@Town", town);
            var insertResults = command.ExecuteNonQuery();

            if (insertResults > 0)
            {
                Console.WriteLine($"Town {town} was added to the database.");
            }
            else
            {
                Console.WriteLine("Alredy exist!");
            }
        }

        private static int townCheck(SqlConnection connection, string[] inputMinion)
        {
            string town = inputMinion[2];
            string query = "SELECT Id FROM Towns";
            query += " WHERE Name=@Town";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@Town", town);
            int results = (int)command.ExecuteScalar();
            return results;
        }

        private static int villainCheck(SqlConnection connection, string villain)
        {
            string villainId = villain;
            string query = "SELECT Id FROM Villains";
            query += " WHERE Name=@villain";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@vallain", villainId);
            int results = (int)command.ExecuteScalar();
            return results;
        }

        private static int minionCheck(SqlConnection connection, string[] inputMinion)
        {
            string minionId = inputMinion[0];
            string query = "SELECT Id FROM Minions";
            query += " WHERE Name=@minion";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@minion", minionId);
            int results = (int)command.ExecuteScalar();
            return results;
        }

        private static void inserMinion(SqlConnection connection, string[] inputMinion, int town)
        {
            string name = inputMinion[0];
            int age = int.Parse(inputMinion[1]);
            int townId = town;

            string minionQuery = "INSERT INTO Minions (Name, Age, TownId)";
            minionQuery += " VALUES (@Name, @Age, @TownId)";
            SqlCommand command = new SqlCommand(minionQuery, connection);
            command.Parameters.AddWithValue("@Name", name);
            command.Parameters.AddWithValue("@Age", age);
            command.Parameters.AddWithValue("@TownId", townId);
            var result = command.ExecuteNonQuery();

            if (result > 0)
            {
                Console.WriteLine($"Successfully added minion with {name}.");
            }
            else
            {
                Console.WriteLine("Error when trying to insert minion!");
            }
        }

        //-------------------------------------------------------
        private static void ChangeTownName(SqlConnection connection)
        {
            var country = Console.ReadLine();

            using (connection)
            {
                string query = File.ReadAllText(@"../../getTowns.sql");
                SqlCommand findTownsByCountry = new SqlCommand(query, connection);
                SqlParameter countryName = new SqlParameter("@Country", country);
                findTownsByCountry.Parameters.Add(countryName);

                var reader = findTownsByCountry.ExecuteReader();

                if (reader.Read())
                {
                    List<string> towns = new List<string>();
                    while (reader.Read())
                    {
                        string currentTown = (string)reader[0];
                        towns.Add(currentTown.ToUpper());
                    }
                    Console.WriteLine($"[{String.Join(",", towns)}]");
                }
                else
                {
                    Console.WriteLine("No town names were affected.");
                }
            }
        }

        //-------------------------------------------------------
        private static void DeleteVillain(SqlConnection connection)
        {
            int villainId = int.Parse(Console.ReadLine());

            using (connection)
            {
                string query = File.ReadAllText($"../../getMinions.sql");
                SqlCommand command = new SqlCommand(query, connection);
                SqlParameter param = new SqlParameter("@villainId", villainId);
                command.Parameters.Add(param);
                int affectedMinions = int.Parse(command.ExecuteScalar().ToString());

                query = File.ReadAllText($"../../getVillainName.sql");
                command = new SqlCommand(query, connection);
                param = new SqlParameter("@villainId", villainId);
                command.Parameters.Add(param);
                string villainName = command.ExecuteScalar().ToString();

                if (villainName != "None")
                {
                    Console.WriteLine($"{villainName} was deleted");
                    Console.WriteLine($"{affectedMinions} minions released");
                }
                else
                {
                    Console.WriteLine("No such villain was found");
                }

                query = File.ReadAllText($"../../deleteVillain.sql");
                command = new SqlCommand(query, connection);
                param = new SqlParameter("@villainId", villainId);
                command.Parameters.Add(param);
                command.ExecuteNonQuery();
            }
        }

        //-------------------------------------------------------
        private static void AllMinionNames(SqlConnection connection)
        {
            using (connection)
            {
                string query = "SELECT Name FROM Minions";
                string count = "SELECT COUNT(Name) FROM Minions";
                SqlCommand printMinionsNames = new SqlCommand(query, connection);
                SqlCommand namesCount = new SqlCommand(count, connection);
                int namesLength = (int)namesCount.ExecuteScalar();
                var reader = printMinionsNames.ExecuteReader();
                List<string> minions = new List<string>();

                while (reader.Read())
                {
                    minions.Add(reader[0].ToString());
                }

                bool firstLast = true;

                while (minions.Count > 0)
                {
                    if (firstLast)
                    {
                        Console.WriteLine(minions.First());
                        minions.Remove(minions.First());
                        firstLast = false;
                    }
                    else
                    {
                        Console.WriteLine(minions.Last());
                        minions.Remove(minions.Last());
                        firstLast = true;
                    }
                }
            }
        }

        //-------------------------------------------------------
        private static void IncreaseMinionsAge(SqlConnection connection)
        {
            int[] minionIds = Console.ReadLine().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).
            ToArray();

            using (connection)
            {
                string query = "SELECT Id, Name, Age FROM Minions";
                SqlCommand loadMinions = new SqlCommand(query, connection);
                SqlDataReader reader = loadMinions.ExecuteReader();
                TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;
                while (reader.Read())
                {
                    for (int i = 0; i < minionIds.Length; i++)
                    {
                        if (minionIds[i] == (int)reader[0])
                        {
                            Console.WriteLine("{0} {1}", textInfo.ToTitleCase((string)reader[1]), (int)reader[2] + 1);
                            break;
                        }
                        else
                        {
                            Console.WriteLine("{0} {1}", (string)reader[1], (int)reader[2]);
                            return;
                        }
                    }
                }
            }
        }

        //-------------------------------------------------------
        private static void IncreaseAgeProc(SqlConnection connection)
        {
            int minionId = int.Parse(Console.ReadLine());

            using (connection)
            {
                string updateQuery = "exec usp_increaseAgeByOne @minionId";
                SqlCommand increaseAge = new SqlCommand(updateQuery, connection);
                SqlParameter Id = new SqlParameter("@minionId", minionId);
                increaseAge.Parameters.Add(Id);
                increaseAge.ExecuteNonQuery();

                string getQuery = "SELECT Name, Age FROM Minions WHERE Id = @minionId";
                SqlCommand getMinion = new SqlCommand(getQuery, connection);
                SqlParameter minId = new SqlParameter("@minionId", minionId);
                getMinion.Parameters.Add(minId);
                SqlDataReader result = getMinion.ExecuteReader();

                while (result.Read())
                {
                    Console.WriteLine($"{result["Name"]} {result["Age"]}");
                }
            }
        }
    }
}
