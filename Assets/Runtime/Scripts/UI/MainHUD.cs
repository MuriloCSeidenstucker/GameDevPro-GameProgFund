using System;
using TMPro;
using UnityEngine;

public class MainHUD : MonoBehaviour
{
    [SerializeField] private PlayerController player;
    [SerializeField] private GameMode gameMode;

    [Header("Overlays")]
    [SerializeField] private GameObject hudOverlay;
    [SerializeField] private GameObject pauseOverlay;
    [SerializeField] private GameObject startGameOverlay;

    [Space]
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI travelledDistanceText;
    [SerializeField] private TextMeshProUGUI countdownText;

    private float countdownTime;
    private int lastCountdown = -1;

    private void Awake()
    {
        ShowStartGameOverlay();
        countdownTime = gameMode.CountdownTime;
    }

    private void LateUpdate()
    {
        scoreText.text = $"SCORE : {player.Score}";
        travelledDistanceText.text = $"{Mathf.RoundToInt(player.TravelledDistance)} m";
        countdownText.text = UpdateCountdownText();
    }

    private string UpdateCountdownText()
    {
        string text = string.Empty;
        if (gameMode.IsGameStarting && countdownTime >= 0)
        {
            countdownText.gameObject.SetActive(true);
            int countdown = (int)(countdownTime -= Time.deltaTime);
            text = $"{countdown+1}";

            if (lastCountdown != countdown)
            {
                countdownText.transform.localScale = Vector3.one;
                lastCountdown = countdown;
            }
            Vector3 scale = countdownText.transform.localScale;
            float t = countdownTime - countdown;
            int maxScale = 5;
            countdownText.transform.localScale = Vector3.Lerp(scale, Vector3.one * maxScale, t * Time.deltaTime);
        }
        else
        {
            countdownText.gameObject.SetActive(false);
        }

        return text;
    }

    private void ShowHudOverlay()
    {
        hudOverlay.SetActive(true);
        pauseOverlay.SetActive(false);
        startGameOverlay.SetActive(false);
    }

    private void ShowPauseOverlay()
    {
        hudOverlay.SetActive(false);
        pauseOverlay.SetActive(true);
        startGameOverlay.SetActive(false);
    }

    private void ShowStartGameOverlay()
    {
        hudOverlay.SetActive(false);
        pauseOverlay.SetActive(false);
        startGameOverlay.SetActive(true);
    }

    public void StartGame()
    {
        ShowHudOverlay();
        gameMode.StartGame();
    }

    public void PauseGame()
    {
        ShowPauseOverlay();
        gameMode.PauseGame();
    }

    public void ResumeGame()
    {
        ShowHudOverlay();
        gameMode.ResumeGame();
    }
}
