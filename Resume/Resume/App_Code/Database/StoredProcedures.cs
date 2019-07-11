using System;
using System.Data.SqlClient;
using System.Data;


namespace Resume.Database
{
    public static class IPQueries
    {
        private static string ConnString()
        {
            //capstonefinal.database.windows.net
            //jhawkins
            //Serverpass1!
            return "X"; //connection string

        }


        /// <summary>
        /// Calls stored procedure to check if IP is new from a user visiting the page.  
        /// If so, the IP will be added to the database and returns true, otherwise returns false.
        /// </summary>
        /// <param name="ipAddress"></param>
        /// <returns></returns>
        public static bool IsNewIP (string ipAddress)
        {
            if (!string.IsNullOrEmpty(ipAddress))
            {
                using (var conn = new SqlConnection(ConnString()))
                using (var comm = new SqlCommand("IPCheck", conn)
                {
                    CommandType = CommandType.StoredProcedure
                })
                {
                    var returnParameter = comm.Parameters.Add("@Exists", SqlDbType.Int);
                    returnParameter.Direction = ParameterDirection.ReturnValue;
                    comm.Parameters.AddWithValue("@IP", ipAddress);
                    conn.Open();
                    comm.ExecuteNonQuery();
                    
                    var result = returnParameter.Value;
                    if (result.ToString() == "1")
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                    
                }
            }
            return false;

        }
    }
}