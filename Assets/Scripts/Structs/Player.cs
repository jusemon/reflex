using System;
using UnityEngine;

[Serializable]
public struct Player
{
    public Player(string name, int score, Sprite flag = null)
    {
        this.name = name;
        this.score = score;
        this.flag = flag;
    }
    public string name;
    public int score;
    public Sprite flag;
}

[Serializable]
public struct PlayerResponse
{
    public string id;
    public string name;
    public string country;
    public int score;
    public DateTime createdAt;
}
