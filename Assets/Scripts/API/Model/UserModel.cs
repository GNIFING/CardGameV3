using System;

public class User
{
    public int Id;
    public string Username;
    public string Role;
    public int Rank;
    public string Address;
    public bool IsActive;
    public DateTime createdAt;
    public DateTime updatedAt;
}

public class IsInGameResponse
{
    public bool isInGame;
    public int? playerId;
    public int? arenaId;
}
