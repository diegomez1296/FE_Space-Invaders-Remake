using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerLifesController : MonoBehaviour
{
    private Image[] lifes;

    private void Start() {
        lifes = GetComponentsInChildren<Image>();
        CheckPlayerLifes(3);
    }

    public void CheckPlayerLifes(int HP) {

        switch(HP) {
            case 4:
                lifes[0].gameObject.SetActive(true);
                lifes[1].gameObject.SetActive(true);
                lifes[2].gameObject.SetActive(true);
                lifes[3].gameObject.SetActive(true);
                break;

            case 3:
                lifes[0].gameObject.SetActive(true);
                lifes[1].gameObject.SetActive(true);
                lifes[2].gameObject.SetActive(true);
                lifes[3].gameObject.SetActive(false);
                break;
            case 2:
                lifes[0].gameObject.SetActive(true);
                lifes[1].gameObject.SetActive(true);
                lifes[2].gameObject.SetActive(false);
                lifes[3].gameObject.SetActive(false);
                break;
            case 1:
                lifes[0].gameObject.SetActive(true);
                lifes[1].gameObject.SetActive(false);
                lifes[2].gameObject.SetActive(false);
                lifes[3].gameObject.SetActive(false);
                break;
            case 0:
                lifes[0].gameObject.SetActive(false);
                lifes[1].gameObject.SetActive(false);
                lifes[2].gameObject.SetActive(false);
                lifes[3].gameObject.SetActive(false);
                break;
        }

    }

}
