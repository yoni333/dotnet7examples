using System;
using Microsoft.Data.SqlClient;
using System.Text;
using Microsoft.Extensions.Options;
using mssql_books.Models;

namespace mssql.services
{
    public class SqlService
    {
        public  SqlConnection connection;
        public SqlService(IOptions<MssqlStoreDatabaseSettings> mssqlStoreDatabaseSettings){
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
            builder.DataSource = mssqlStoreDatabaseSettings.Value.DataSource;
            builder.UserID = mssqlStoreDatabaseSettings.Value.UserID;
            builder.Password = mssqlStoreDatabaseSettings.Value.Password;
            builder.InitialCatalog = mssqlStoreDatabaseSettings.Value.InitialCatalog;
            builder.TrustServerCertificate =bool.Parse( mssqlStoreDatabaseSettings.Value.TrustServerCertificate);
            connection = new SqlConnection(builder.ConnectionString);
        }

        
       
    }
}