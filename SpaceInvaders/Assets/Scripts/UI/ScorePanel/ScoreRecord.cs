using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreRecord : MonoBehaviour {
    [SerializeField]
    private TextMeshProUGUI lpText;
    [SerializeField]
    private TextMeshProUGUI userText;
    [SerializeField]
    private TextMeshProUGUI scoreText;

    public void InitScoreRecord(int lp, PlayerScore playerScore) {
        lpText.text = lp + "";
        userText.text = playerScore.Username;
        scoreText.text = playerScore.Score +"";
    }
}
