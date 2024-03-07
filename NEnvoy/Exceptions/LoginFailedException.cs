namespace NEnvoy.Exceptions;

public class LoginFailedException(string? responseMessage) : EnvoyException("Login failed")
{
    public string? ResponseMessage { get; init; } = responseMessage;
}