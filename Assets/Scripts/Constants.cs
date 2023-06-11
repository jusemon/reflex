using System.Linq;

public static class Constants
{
    public readonly static string Jump = "Jump";
    public readonly static string IsJumping = "IsJumping";
    public readonly static string Floor = "Floor";
    public readonly static string Obstacle = "Obstacle";
    public readonly static string Score = "Score";
    public readonly static string HighScore = "HighScore";
    public readonly static string DotFiller = new string(Enumerable.Range(1, 80).Select((_) => '.').ToArray());
}
