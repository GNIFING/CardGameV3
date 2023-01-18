using System;

public class AuthRequest
{
    public string username;
    public string password;
}

public class AuthResponse
{
    public string accessToken;
    public string refreshToken;
}
