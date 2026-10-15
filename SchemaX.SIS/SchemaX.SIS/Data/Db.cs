using System.Text.Json;
using MySqlConnector;

namespace SchemaX.SIS.Data;

public static class Db
{
    private static string? _cs;
    private static string ConnectionString => _cs ??= Load();
    private static string Load()
    {
        string path = Path.Combine(AppContext.BaseDirectory, "appsettings.json");
        using var doc = JsonDocument.Parse(File.ReadAllText(path));
        return doc.RootElement.GetProperty("ConnectionString").GetString()!;
    }

    public static MySqlConnection Open()
    {
        var con = new MySqlConnection(ConnectionString);
        con.Open();
        return con;
    }
}
