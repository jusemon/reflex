using TMPro;
using UnityEngine;

public class ScoreController : MonoBehaviour
{
    private GameController gameController;
    private TextMeshProUGUI textMeshPro;

    void Awake()
    {
        gameController = FindObjectOfType<GameController>();
        textMeshPro = GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        textMeshPro.SetText("Score: " + Mathf.FloorToInt(gameController.score));
    }
}
