namespace NEnvoy.Exceptions;
public class EnvoyException : Exception
{
    public EnvoyException(string message, Exception? innerException = null)
        : base(message, innerException) { }
}
