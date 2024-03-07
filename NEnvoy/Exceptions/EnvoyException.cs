namespace NEnvoy.Exceptions;
public class EnvoyException(string message, Exception? innerException = null) : Exception(message, innerException)
{
}
