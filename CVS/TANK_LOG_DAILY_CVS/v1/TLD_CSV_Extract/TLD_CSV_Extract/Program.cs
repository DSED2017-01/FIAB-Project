using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TLD_CSV_Extract
{
    public class Program
    {
        static void Main(string[] args)
        {
            processData();
        }

        /*
         SELECT TANK.ID_CODE, TANK_LOG.SPECIES_TEXT, TANK_LOG.SPECIES_TEXT_2, 
               REASON_MORTALITY.TEXT, TANK_LOG_DAILY.QTY, TANK_LOG_DAILY.COMMENT
        FROM TANK_LOG_DAILY, TANK_LOG, TANK, REASON_MORTALITY
        WHERE TANK_LOG.ID_PK = TANK_LOG_DAILY.LOG_FK
        AND TANK.ID_PK = TANK_LOG.TANK_FK
        AND TANK_LOG_DAILY.REASON_FK = REASON_MORTALITY.ID_PK
         */


        private static void processData()
        {
            SqlCommand command = new SqlCommand();
            command.CommandText = 
                "SELECT [TANK].ID_CODE, [TANK_LOG].SPECIES_TEXT, [TANK_LOG].SPECIES_TEXT_2, " +
                "[REASON_MORTALITY].TEXT, [TANK_LOG_DAILY].QTY, [TANK_LOG_DAILY].COMMENT " +
                "FROM [TANK_LOG_DAILY], [TANK_LOG], [TANK], [REASON_MORTALITY] " +
                "WHERE [TANK_LOG].ID_PK = [TANK_LOG_DAILY].LOG_FK " +
                "AND [TANK].ID_PK = [TANK_LOG].TANK_FK " +
                "AND [TANK_LOG_DAILY].REASON_FK = [REASON_MORTALITY].ID_PK";
            using (SqlConnection connection = new SqlConnection(Lib.DAO.ConnectionHelper.ConnectionString))
            {
                command.Connection = connection;
                connection.Open();
                try
                {
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            Console.WriteLine($"{reader.GetString(0)}"
                                + $"\t{reader.GetInt32(4)}"
                                + $"\t{reader.GetString(1)}"
                                + $"\t{reader.GetString(2)}"
                                + $"\t{reader.GetString(3)}"
                                
                                );
                        }
                    }
                    else
                    {
                        Console.WriteLine("No rows found.");
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("ERROR : " + ex.Message);
                }
                finally
                {
                    if (connection.State == System.Data.ConnectionState.Open)
                    {
                        connection.Close();
                    }
                }
            }

            Console.WriteLine("end of program");
            Console.Read();
        }
    }
}
