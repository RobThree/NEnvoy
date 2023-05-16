namespace NEnvoy.Exceptions;

public class LoginFailedException : EnvoyException
{
    public string? ResponseMessage { get; init; }

    public LoginFailedException(string? responseMessage)
        : base("Login failed") => ResponseMessage = responseMessage;
}