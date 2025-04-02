using MySql.Data.MySqlClient;
namespace DBConnectLibrary;

public abstract class AccessBDD
{
    protected readonly string ConnectionString =
        "Server=localhost;Port=3306;Database=livin_paris_bdd;Uid=root;Password=root";

    protected MySqlConnection Connection()
    {
        return new MySqlConnection(ConnectionString);
    }
}