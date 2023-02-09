using System;
using System.Collections.Generic;

public class CreateUserRequest
{
    public List<int> cards;
}

[Serializable]
public class UserModel
{
    public int id;
    public string username;
    public string role;
    public int rank;
    public bool isActive;
    public DateTime createdAt;
    public DateTime updatedAt;
}

