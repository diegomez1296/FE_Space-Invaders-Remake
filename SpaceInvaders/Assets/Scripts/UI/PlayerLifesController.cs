using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerLifesController : MonoBehaviour
{
    private const int HP_LIMIT = 4;
    private Image[] lifes;

    private void Start() {
        lifes = GetComponentsInChildren<Image>();
        CheckPlayerLifes(3);
    }

    public void CheckPlayerLifes(int HP) {

        ClearHP();
        for (int i = 0; i < (HP < HP_LIMIT ? HP : HP_LIMIT); i++) {
            lifes[i].gameObject.SetActive(true);
        }
    }

    private void ClearHP() {
        foreach (var item in lifes) {
            item.gameObject.SetActive(false);
        }
    }

}
