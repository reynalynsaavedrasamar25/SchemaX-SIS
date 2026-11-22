namespace SchemaX.Mobile.Services;

/// <summary>A friendly, user-safe error. Technical details stay in InnerException.</summary>
public class DataAccessException : Exception
{
    public DataAccessException(string message, Exception? inner = null) : base(message, inner) { }
}