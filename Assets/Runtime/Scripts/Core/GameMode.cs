using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMode : MonoBehaviour
{
    [Header("Player")]
    [SerializeField] private PlayerController player;
    [SerializeField] private PlayerAnimationController playerAnim;

    [Header("UI")]
    [SerializeField] private MusicPlayer musicPlayer;

    [Header("Gameplay")]
    [SerializeField] private float startPlayerSpeed = 10f;
    [SerializeField] private float maxPlayerSpeed = 17f;
    [SerializeField] private float timeToMaxSpeedSeconds = 300f;
    [SerializeField] private float reloadGameDelay = 3f;
    [Range(0, 5, order = 1)]
    [SerializeField] private int countdownTime = 3;

    [Header("Score")]
    [SerializeField] private float baseScoreMultiplier = 1f;
    private float score;
    public int Score => Mathf.RoundToInt(score);

    public float CountdownTime => countdownTime;
    public bool IsGamePaused { get; private set; }
    public bool IsGameStarting { get; private set; }
    public bool IsGameOver { get; private set; }

    private float startGameOverMusicDelay = 0.3f;
    private float startGameTime;

    private void Awake()
    {
        IsGameOver = false;
        player.enabled = false;
        musicPlayer.PlayStartMenuMusic();
    }

    private void Update()
    {
        if (player.enabled == false && playerAnim.IsGameStartAnimFinished == true)
        {
            player.ForwardSpeed = startPlayerSpeed;
            player.enabled = true;
            startGameTime = Time.time;
        }

        if (player.enabled && !IsGameOver)
        {
            score += baseScoreMultiplier * player.ForwardSpeed * Time.deltaTime;
            float timePercent = (Time.time - startGameTime) / timeToMaxSpeedSeconds;
            player.ForwardSpeed = Mathf.Lerp(startPlayerSpeed, maxPlayerSpeed, timePercent);
        }
    }

    private IEnumerator ReloadGameCoroutine()
    {
        yield return new WaitForSeconds(startGameOverMusicDelay);
        musicPlayer.PlayGameOverTrackMusic();
        yield return new WaitForSeconds(reloadGameDelay);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private IEnumerator StartGameCor(float time)
    {
        musicPlayer.PlayMainTrackMusic();
        yield return new WaitForSeconds(time);
        playerAnim.StartGameAnim();
        IsGameStarting = false;
        IsGameOver = false;
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
        IsGameOver = true;
        StartCoroutine(ReloadGameCoroutine());
    }
}
