using UnityEngine;

[System.Serializable]
public struct Player
{
    public Player(string name, int score, Sprite flag)
    {
        this.name = name;
        this.score = score;
        this.flag = flag;
    }
    public string name;
    public int score;
    public Sprite flag;
}
