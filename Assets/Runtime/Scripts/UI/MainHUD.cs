using TMPro;
using UnityEngine;

public class MainHUD : MonoBehaviour
{
    [SerializeField] private PlayerController player;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI travelledDistanceText;

    private void LateUpdate()
    {
        scoreText.text = $"SCORE : {player.Score}";
        travelledDistanceText.text = $"{player.TravelledDistance} m";
    }
}
