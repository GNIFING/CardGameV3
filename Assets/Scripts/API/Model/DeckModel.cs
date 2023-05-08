public class Deck
{
    public int id;
    public string name;
    public UserCard[] userCards;
    public CardClass className;
}

public class DeckItem
{
    public int id;
    public string name;
}

public class CreateDeckRequest
{
    public string name;
    public string className;
    public int[] cards;
}
public class CreateDeckResponse
{
    public int id;
    public string name;
    public Card[] cards;
}
public class UpdateDeckCardRequest
{
    public int id;
    public int cardId;
}