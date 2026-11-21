using MySqlConnector;
using System.Text.Json;
using Microsoft.Maui.Storage;

namespace SchemaX.Mobile.Services;

public class DbConfig
{
#if ANDROID
    // Android emulator's alias for the host PC's loopback address
    public string Host { get; set; } = "10.0.2.2";
#else
    public string Host { get; set; } = "127.0.0.1";
#endif

    public uint Port { get; set; } = 3306;
    public string Database { get; set; } = "schemax_sis";
    public string User { get; set; } = "schemax_mobile";
    public string Password { get; set; } = "CHANGE_ME";   // real value comes from appsettings.json

    // Fail fast so the app shows an offline message instead of hanging
    public uint TimeoutSeconds { get; set; } = 5;

    // Reads Resources/Raw/appsettings.json. Missing values keep the defaults above.
    public static DbConfig Load()
    {
        try
        {
            using var stream = FileSystem.OpenAppPackageFileAsync("appsettings.json")
                                         .GetAwaiter().GetResult();
            var loaded = JsonSerializer.Deserialize<DbConfig>(
                stream, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            if (loaded != null)
            {
                System.Diagnostics.Debug.WriteLine($"[CFG] file loaded, password length = {loaded.Password.Length}");
                return loaded;
            }
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"[CFG] could not load appsettings.json: {ex.GetType().Name}: {ex.Message}");
        }

        System.Diagnostics.Debug.WriteLine("[CFG] using default password");
        return new DbConfig();
    }

    public string BuildConnectionString() =>
        new MySqlConnectionStringBuilder
        {
            Server = Host,
            Port = Port,
            Database = Database,
            UserID = User,
            Password = Password,
            ConnectionTimeout = TimeoutSeconds,
            SslMode = MySqlSslMode.None,
            AllowPublicKeyRetrieval = true
        }.ConnectionString;
}