public class ArenaPlayerRequest
{
    public int arenaId;
    public int playerId;
}

public class DrawCardResponse
{
    public int id;
    public int deckId;
    public UserCard[] cards;
    public int mana;
    public bool isTurn;
}

public class CreatePlayerRequest
{
    public int deckId;
}

public class CreatePlayerResponse
{
    public int id;
    public int deckId;
    public int hp;
    public int mana;
    public bool isTurn;
}

public class LaydownCardRequest
{
    public int arenaId;
    public int cardId;
    public int index;
}

public class SurrenderResponse
{
    public string message;
}

public class AttackCardRequest
{
    public int arenaId;
    public int attackerIndex;
    public int defenderIndex;
}

public class AttackCardReponse
{
    public UserCard attackerUserCard;
    public UserCard defenderUserCard;
}

public class UpdateCardRequest
{
    public int arenaId;
    public int cardIndex;
    public int hp;
    public int atk;
}

public class MarkUseCardRequest
{
    public int arenaId;
    public int cardIndex;
}

public class AttackTowerRequest
{
    public int arenaId;
    public int defenderId;
    public int attackerIndex;
}

public class AttackTowerResponse
{
    public int id;
    public int hp;
}

public class MoveCardRequest
{
    public int arenaId;
    public int beforeIndex;
    public int afterIndex;
}

public class EndTurnResponse
{
    public int id;
    public int mana;
    public int hp;
    public bool isTurn;
}