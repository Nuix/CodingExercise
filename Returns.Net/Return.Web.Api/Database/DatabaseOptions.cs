using System.Diagnostics;

namespace Return.Web.Api.Database;

[DebuggerDisplay("{ToString(),nq}")]
public class DatabaseOptions
{
    public const string SectionName = "DatabaseOptions";
    
    public string Host { get; set; }
    
    public int Port { get; set; }
    
    public string? Username { get; set; }
    
    public string? Password { get; set; }

    public override string ToString() => $"DatabaseOptions(Host={Host}, Port={Port}, Username={Username})";

    //options validator
}