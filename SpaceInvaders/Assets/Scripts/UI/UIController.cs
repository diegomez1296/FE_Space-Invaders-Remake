using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public PlayerLifesController PlayerLifes { get; set; }
    public ScoreController Score { get; set; }
    public LevelController Level { get; set; }
    public WaveController Wave { get; set; }
    public Slider Slider { get; set; }
    public RocketController Rocket { get; set; }
    [SerializeField] private TextMeshProUGUI gameOverText;

    private void Start()
    {
        PlayerLifes = GetComponentInChildren<PlayerLifesController>();
        Score = GetComponentInChildren<ScoreController>();
        Level = GetComponentInChildren<LevelController>();
        Wave = GetComponentInChildren<WaveController>();
        Slider = GetComponentInChildren<Slider>();
        SetSliderValues(5);
        Rocket = GetComponentInChildren<RocketController>();
    }

    public void SetSliderValues(int maxV)
    {
        Slider.maxValue = maxV;
        Slider.value = 0;
    }

    public void ActivateGameOverText() {
        gameOverText.gameObject.SetActive(true);
    }
}
