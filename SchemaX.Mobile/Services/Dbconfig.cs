using MySqlConnector;

namespace SchemaX.Mobile.Services;

public class DbConfig
{
#if ANDROID
    // Android emulator's address for the host PC (localhost of your computer)
    public string Host { get; set; } = "10.0.2.2";
#else
    public string Host { get; set; } = "127.0.0.1";
#endif

    public uint Port { get; set; } = 3306;
    public string Database { get; set; } = "schemax_sis";
    public string User { get; set; } = "schemax_mobile";
    public string Password { get; set; } = "YOU_PASSWORD";

    // Fail fast so the app shows an offline message instead of hanging
    public uint TimeoutSeconds { get; set; } = 5;

    public string BuildConnectionString() =>
        new MySqlConnectionStringBuilder
        {
            Server = Host,
            Port = Port,
            Database = Database,
            UserID = User,
            Password = Password,
            ConnectionTimeout = TimeoutSeconds
        }.ConnectionString;
}