namespace mssql_books.Models;

public class MssqlStoreDatabaseSettings
{

    public string DataSource { get; set; } = null!;
    public string UserID { get; set; } = null!;
    public string Password { get; set; } = null!;
    public string InitialCatalog { get; set; } = null!;
    public string TrustServerCertificate { get; set; } = null!;

}