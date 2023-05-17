namespace NEnvoy.Models;

public class SessionInfo
{
    public required string Token { get; init; }
    public required string Id { get; init; }
    public bool IsConsumer { get; init; } = true;
}