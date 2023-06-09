using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class GameOverController : MonoBehaviour
{
    private TextMeshProUGUI textMeshPro;
    private ScoreController score;

    private void Awake()
    {
        textMeshPro = GetComponent<TextMeshProUGUI>();
        score = FindObjectOfType<ScoreController>();
    }

    private void OnEnable()
    {
        var lastScore = PlayerPrefs.GetInt(Constants.Score);
        if (PlayerPrefs.HasKey(Constants.HighScore))
        {
            if (lastScore > PlayerPrefs.GetInt(Constants.HighScore))
            {
                PlayerPrefs.SetInt(Constants.HighScore, lastScore);
                textMeshPro.text = "New High Score " + lastScore + "!";
            }
            else
            {
                textMeshPro.text = "Score " + lastScore;
            }
        }
        else
        {
            PlayerPrefs.SetInt(Constants.HighScore, lastScore);
            textMeshPro.text = "New High Score " + lastScore + "!";
        }
        PlayerPrefs.Save();
    }
}
