using UnityEngine;

[RequireComponent(typeof(GameController))]
public class MusicController : MonoBehaviour
{
    private AudioSource audioSource;
    private GameController gameController;

    void Start()
    {
        gameController = GetComponent<GameController>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (gameController.paused)
        {
            // Pause music
            audioSource.Pause();
            return;
        }
        // Resume music
        if (audioSource.isPlaying)
        {
            audioSource.UnPause();
        }
        else
        {
            audioSource.Play();
        }
    }
}
