namespace SchemaX.Mobile.Services;

/// <summary>Holds the logged-in user's id. Every query filters by it (decision D1).</summary>
public class UserSession
{
    public int UserId { get; set; } = 1;   // temporary demo user until LoginAsync is finished
}