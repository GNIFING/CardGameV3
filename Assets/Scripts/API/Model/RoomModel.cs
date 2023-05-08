public class CreateMatchMakingRequest
{
    public int playerId;
}

public class CreateMatchMakingResponse
{
    public string roomId;
    public int playerOndId;
    public int? playerTwoId;
    public int? arenaId;
}

public class CancelMatchMakingRequest
{
    public string roomId;
    public int playerId;
}
public class CancelMatchMakingResponse
{
    public string roomId;
    public int playerId;
    public int? playerTwoId;
    public bool isActive;
}