using TMPro;
using UnityEngine;

public class MainHUD : MonoBehaviour
{
    [SerializeField] private PlayerController player;
    [SerializeField] private GameMode gameMode;
    [SerializeField] private GameObject hudOverlay;
    [SerializeField] private GameObject pauseOverlay;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI travelledDistanceText;

    private void Awake()
    {
        ShowHudOverlay();
    }

    private void LateUpdate()
    {
        scoreText.text = $"SCORE : {player.Score}";
        travelledDistanceText.text = $"{Mathf.RoundToInt(player.TravelledDistance)} m";
    }

    private void ShowHudOverlay()
    {
        hudOverlay.SetActive(true);
        pauseOverlay.SetActive(false);
    }

    private void ShowPauseOverlay()
    {
        hudOverlay.SetActive(false);
        pauseOverlay.SetActive(true);
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
