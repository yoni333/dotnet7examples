using System.Data;
using Microsoft.Data.SqlClient;

namespace mssql.services;

public class MssqlCalls
{

    public   SqlService _sqlService;

    public MssqlCalls(SqlService sqlService) => _sqlService=  sqlService;


    public List<string> SelectTable()
    {
        List<string> list = new();
        try
        {


            Console.WriteLine("\nQuery data example:");
            Console.WriteLine("=========================================\n");

            String sql = "SELECT name ,id FROM books";

            using (SqlCommand command = new SqlCommand(sql, _sqlService.connection))
            {
                _sqlService.connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(reader.GetString(0) + " " + reader.GetInt32(1));
                        Console.WriteLine("{0} {1}", reader.GetString(0), reader.GetInt32(1));

                    }
                    Console.WriteLine(list.ToString());
                }
            }

        }
        catch (SqlException e)
        {
            Console.WriteLine(e.ToString());
        }
        Console.WriteLine("end");
        return list;

    }

    public List<string> SelectProcedure(int id)
    {
        List<string> list = new();
        try
        {


            Console.WriteLine("\nQuery procedure example:");
            Console.WriteLine("=========================================\n");

            String sql = "get_item_by_id";

            using (SqlCommand command = new SqlCommand(sql, _sqlService.connection))
            {

                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add(new SqlParameter("@id", SqlDbType.Int) { Value = id });

                _sqlService.connection.Open();
                
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(reader.GetString(0) + " " + reader.GetInt32(1));
                        Console.WriteLine("{0} {1}", reader.GetString(0), reader.GetInt32(1));

                    }
                    Console.WriteLine(list.ToString());
                }
            }

        }
        catch (SqlException e)
        {
            Console.WriteLine(e.ToString());
        }
        Console.WriteLine("end");
        return list;

    }
}
