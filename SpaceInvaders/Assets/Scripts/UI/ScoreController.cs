using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreController : MonoBehaviour
{
    private TextMeshProUGUI scoreValue;
    private int score;

    private void Start() {
        score = 0;
        scoreValue = GetComponentsInChildren<TextMeshProUGUI>()[1];
    }

    public void AddScoreValue(int newScore) {
        score += newScore;

        if (score < 10) {
            scoreValue.text = "0000" + score;
            return;
        }
            
        if (score < 100) {
            scoreValue.text = "000" + score;
            return;
        }

        if (score < 1000) {
            scoreValue.text = "00" + score;
            return;
        }

        if (score < 10000) {
            scoreValue.text = "0" + score;
            return;
        }

        scoreValue.text = ""+score;
    }
}
