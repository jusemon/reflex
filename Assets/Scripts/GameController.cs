using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    [SerializeField] public float speed = 5f;
    [SerializeField] public float increment = 0.01f;
    [SerializeField] public float score = 0f;
    [SerializeField] public bool paused = false;
    [SerializeField] public string sceneName = string.Empty;
    [SerializeField] public GameObject gameOverCanvas;

    private void Awake()
    {
        if (string.IsNullOrEmpty(sceneName))
        {
            sceneName = SceneManager.GetActiveScene().name;
        }
    }

    private void Update()
    {
        if (paused)
        {
            if (Input.GetButtonDown(Constants.Jump))
            {
                SceneManager.LoadScene(sceneName);
            }
            return;
        }
        speed += increment * Time.deltaTime;
        score += Time.deltaTime;

    }

    public void ShowGameOver()
    {
        var newScore = Mathf.FloorToInt(score);
        PlayerPrefs.SetInt(Constants.Score, newScore);
        PlayerPrefs.Save();
        paused = true;
        gameOverCanvas.SetActive(true);
    }
}
