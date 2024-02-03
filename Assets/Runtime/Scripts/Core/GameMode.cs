using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMode : MonoBehaviour
{
    [SerializeField] private PlayerController player;
    [SerializeField] private PlayerAnimationController playerAnim;
    [SerializeField] private MusicPlayer musicPlayer;
    [SerializeField] private float reloadGameDelay = 3f;
    [SerializeField] private float countdownTime = 3f;

    public float CountdownTime => countdownTime;
    public bool IsGamePaused { get; private set; }
    public bool IsGameStarting { get; private set; }

    private void Awake()
    {
        player.enabled = false;
        musicPlayer.PlayStartMenuMusic();
    }

    private IEnumerator ReloadGameCoroutine()
    {
        //esperar uma frame
        yield return new WaitForSeconds(reloadGameDelay);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private IEnumerator StartGameCor(float time)
    {
        musicPlayer.PlayMainTrackMusic();
        yield return new WaitForSeconds(time);
        playerAnim.StartGameAnim();
        IsGameStarting = false;
    }

    public void StartGame()
    {
        IsGameStarting = true;
        StartCoroutine(StartGameCor(countdownTime));
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
        IsGamePaused = true;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        IsGamePaused = false;
    }

    public void OnGameOver()
    {
        StartCoroutine(ReloadGameCoroutine());
    }
}
