using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameMode : MonoBehaviour
{
    [SerializeField] private GameObject mainHUD;
    [SerializeField] private GameObject pauseHUD;
    [SerializeField] private float reloadGameDelay = 3;

    public bool IsGamePaused { get; private set; }

    private void Awake()
    {
        EnableMainHUD();
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
        IsGamePaused = true;
        EnablePauseHUD();
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        IsGamePaused = false;
        EnableMainHUD();
    }

    public void OnGameOver()
    {
        StartCoroutine(ReloadGameCoroutine());
    }

    private IEnumerator ReloadGameCoroutine()
    {
        //esperar uma frame
        yield return new WaitForSeconds(reloadGameDelay);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void EnableMainHUD()
    {
        mainHUD.SetActive(true);
        pauseHUD.SetActive(false);
    }

    private void EnablePauseHUD()
    {
        mainHUD.SetActive(false);
        pauseHUD.SetActive(true);
    }
}
