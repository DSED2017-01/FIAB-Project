using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Excel_ImportWPF.DAO
{
    public class DAOHelper
    {
        public static void InsertData(SqlCommand command)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionHelper.ConnectionString))
            {
                command.Connection = connection;
                connection.Open();
                try
                {
                    int rowAffected = command.ExecuteNonQuery();
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
        }

        public static int RetreiveID(SqlCommand command)
        {
            int id = -1;

            using (SqlConnection connection = new SqlConnection(ConnectionHelper.ConnectionString))
            {
                command.Connection = connection;
                connection.Open();
                try
                {
                    SqlDataReader reader = command.ExecuteReader();

                    if(reader.HasRows)
                    {
                        reader.Read();
                        id = (int)reader[0];
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

            return id;
        }

        public static string RetreiveString(SqlCommand command)
        {
            string result = string.Empty;

            using (SqlConnection connection = new SqlConnection(ConnectionHelper.ConnectionString))
            {
                command.Connection = connection;
                connection.Open();
                try
                {
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        reader.Read();
                        result = (string)reader[0];
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

            return result;
        }

        public static List<string> RetreiveAllString(SqlCommand command, int noString)
        {
            List<string> result = new List<string>();

            using (SqlConnection connection = new SqlConnection(ConnectionHelper.ConnectionString))
            {
                command.Connection = connection;
                connection.Open();
                try
                {
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        reader.Read();
                        //result = (string)reader[0];
                        //return re
                        for (int i = 0; i < noString; i++)
                        {
                            //if(reader[i].)
                            result.Add(reader[i].ToString());
                        }

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

            return result;
        }
    }
}
