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

    private UIAudioController audioController;
    private float countdownTime;
    private int lastCountdown = -1;

    private void Awake()
    {
        audioController = GetComponent<UIAudioController>();
        ShowStartGameOverlay();
        countdownTime = gameMode.CountdownTime;
    }

    private void LateUpdate()
    {
        scoreText.text = $"SCORE : {player.Score}";
        travelledDistanceText.text = $"{Mathf.RoundToInt(player.TravelledDistance)} m";
        if (gameMode.IsGameStarting)
        {
            countdownText.text = UpdateCountdownText();
        }
    }

    private string UpdateCountdownText()
    {
        string text = string.Empty;
        if (countdownTime >= 0)
        {
            countdownText.gameObject.SetActive(true);
            int countdown = (int)(countdownTime -= Time.deltaTime);
            text = $"{countdown+1}";

            if (lastCountdown != countdown)
            {
                countdownText.transform.localScale = Vector3.one;
                lastCountdown = countdown;
                audioController.PlayCountdownSFX();
            }
            Vector3 scale = countdownText.transform.localScale;
            float t = countdownTime - countdown;
            int maxScale = 5;
            countdownText.transform.localScale = Vector3.Lerp(scale, Vector3.one * maxScale, t * Time.deltaTime);
        }

        if (countdownTime <= 0)
        {
            countdownText.gameObject.SetActive(false);
            audioController.PlayCountdownEndSFX();
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
        audioController.PlayButtonClickSFX();
        ShowHudOverlay();
        gameMode.StartGame();
    }

    public void PauseGame()
    {
        audioController.PlayButtonClickSFX();
        ShowPauseOverlay();
        gameMode.PauseGame();
    }

    public void ResumeGame()
    {
        audioController.PlayButtonClickSFX();
        ShowHudOverlay();
        gameMode.ResumeGame();
    }
}
