using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusController : MonoBehaviour
{
    private const int BONUS_AMOUNT = 3;
    private BonusBehaviour[] bonuses;


    private void Awake() {
        bonuses = GetComponentsInChildren<BonusBehaviour>();
    }

    private void Start() {
        this.gameObject.SetActive(false);
    }

    public void RandBonus(int percentForBonus, Vector2 enemyPosition) {

        if((100 - percentForBonus) <= Random.Range(0, 100)) {

            switch (Random.Range(0, BONUS_AMOUNT)) {

                case 0:
                    InitBonus(0, enemyPosition);
                    break;
                case 1:
                    InitBonus(1, enemyPosition);
                    break;
                case 2:
                    InitBonus(2, enemyPosition);
                    break;
                default:
                    break;
            }
        }
    }

    private void InitBonus(int idx, Vector2 enemyPosition) {
        BonusBehaviour copyBonus = Instantiate(bonuses[idx]);
        copyBonus.transform.position = enemyPosition;
        copyBonus.gameObject.SetActive(true);
    }
}
