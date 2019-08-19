using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIController : MonoBehaviour
{
    public PlayerLifesController PlayerLifes { get; set; }
    public ScoreController Score { get; set; }
    [SerializeField] private TextMeshProUGUI gameOverText;

    private void Start()
    {
        PlayerLifes = GetComponentInChildren<PlayerLifesController>();
        Score = GetComponentInChildren<ScoreController>();
    }

    public void ActivateGameOverText() {
        gameOverText.gameObject.SetActive(true);
    }
}
