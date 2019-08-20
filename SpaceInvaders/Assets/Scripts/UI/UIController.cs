using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIController : MonoBehaviour
{
    public PlayerLifesController PlayerLifes { get; set; }
    public ScoreController Score { get; set; }
    public LevelController Level { get; set; }
    public WaveController Wave { get; set; }
    [SerializeField] private TextMeshProUGUI gameOverText;

    private void Start()
    {
        PlayerLifes = GetComponentInChildren<PlayerLifesController>();
        Score = GetComponentInChildren<ScoreController>();
        Level = GetComponentInChildren<LevelController>();
        Wave = GetComponentInChildren<WaveController>();
    }

    public void ActivateGameOverText() {
        gameOverText.gameObject.SetActive(true);
    }
}
