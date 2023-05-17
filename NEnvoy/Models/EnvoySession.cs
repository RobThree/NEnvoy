public record EnvoySession(
    string Token,
    string Id,
    bool IsConsumer
) {
    public static readonly EnvoySession NullSession = new(string.Empty, string.Empty, true);
}