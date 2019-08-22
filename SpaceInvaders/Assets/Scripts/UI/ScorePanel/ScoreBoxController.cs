using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreBoxController : MonoBehaviour
{
    private static int HIGH_SCORES_COUNT = 7;
    [SerializeField]
    private ScoreRecord scoreRecord;

    public void SetHighScores(List<PlayerScore> sortedlistOfPlayerScores) {

        for (int i = 0; i < (sortedlistOfPlayerScores.Count > HIGH_SCORES_COUNT ? HIGH_SCORES_COUNT : sortedlistOfPlayerScores.Count); i++) {
            var scoreRecordCopy = Instantiate(scoreRecord, scoreRecord.transform.parent);
            scoreRecordCopy.InitScoreRecord(i+1, sortedlistOfPlayerScores[i]);
            scoreRecordCopy.gameObject.SetActive(true);
        }
    }
}
