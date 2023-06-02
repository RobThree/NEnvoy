namespace NEnvoy.Exceptions;

public class NoActiveSessionException : EnvoyException
{
    public NoActiveSessionException()
        : base("No active session") { }
}