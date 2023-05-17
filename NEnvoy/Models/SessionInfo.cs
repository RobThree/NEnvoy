public class SessionInfo {
    public required string Token { get; init; }
    public required string Id { get; init; }
    public required bool IsConsumer { get; init; } = true;
}