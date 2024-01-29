using TMPro;
using UnityEngine;

public class MainHUD : MonoBehaviour
{
    [SerializeField] private PlayerController player;
    [SerializeField] private TextMeshProUGUI scoreText;

    private void LateUpdate()
    {
        scoreText.text = $"SCORE : {player.Score}";
    }
}
