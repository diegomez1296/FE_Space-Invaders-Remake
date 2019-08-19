using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerLifesController : MonoBehaviour
{
    private Image[] lifes;

    private void Start() {
        lifes = GetComponentsInChildren<Image>();
    }

    public void CheckPlayerLifes(int HP) {

        switch(HP) {
            case 3:
                lifes[0].gameObject.SetActive(true);
                lifes[1].gameObject.SetActive(true);
                lifes[2].gameObject.SetActive(true);
                break;
            case 2:
                lifes[0].gameObject.SetActive(true);
                lifes[1].gameObject.SetActive(true);
                lifes[2].gameObject.SetActive(false);
                break;
            case 1:
                lifes[0].gameObject.SetActive(true);
                lifes[1].gameObject.SetActive(false);
                lifes[2].gameObject.SetActive(false);
                break;
            case 0:
                lifes[0].gameObject.SetActive(false);
                lifes[1].gameObject.SetActive(false);
                lifes[2].gameObject.SetActive(false);
                break;
        }

    }

}
