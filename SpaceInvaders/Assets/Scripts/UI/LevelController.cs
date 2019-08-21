using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    private TextMeshProUGUI levelValue;
    private int level = 1;

    private void Start() {
        levelValue = GetComponentsInChildren<TextMeshProUGUI>()[1];
    }

    public void CheckUILevelValue() {
        if(level != GameController.GameLevel) {
            level = GameController.GameLevel;
            levelValue.text = "" + level;
            GetComponentInParent<UIController>().SetSliderValues(GameController.GameLevel * GameController.GameLevel * 5 - GameController.EnemyKills);
        }
    }
}
