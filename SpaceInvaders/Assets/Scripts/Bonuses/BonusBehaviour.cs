using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BonusBehaviour : MonoBehaviour
{
    public const int HP_LIMIT = 4;
    public const float SPEED_BULLET_LIMIT = 0.3f;

    [SerializeField]
    private PlayerBehaviour player;
    [SerializeField]
    private BonusType bonusType;
    [SerializeField]
    private GameObject bullet;

    private void BonusEffect() {

        switch (bonusType) {
            case BonusType.HP:
                if (player.HP < HP_LIMIT) {
                    player.HP += 1;
                    player.ui.PlayerLifes.CheckPlayerLifes(player.HP);
                }
                break;
            case BonusType.BULLET_SPEED:
                if(player.GetComponent<PlayerController>().SpeedBulletMod < SPEED_BULLET_LIMIT) {
                    player.GetComponent<PlayerController>().SpeedBulletMod += 0.05f;
                }
                break;
            case BonusType.WEAPON_UPGRADE:
                Instantiate(bullet, bullet.transform.parent);
                CheckNumberOfBullets();
                break;
            default:
                break;
        }
    }

    private void CheckNumberOfBullets() {

        var BulletsTab = bullet.transform.parent.GetComponentsInChildren<BulletBase>();
        Vector2 vector2 = bullet.transform.localPosition;
        Debug.Log(vector2);
        Debug.Log(BulletsTab.Length);
        if (BulletsTab.Length % 2 == 0) {
            float x = 0 - BulletsTab.Length / 2;
            Debug.Log("x: " + x);
            foreach (var item in BulletsTab) {
                item.transform.position = new Vector2(x- 2, 0);
                Debug.Log("transform.pPosition: " + transform.position);
                x -=1.1f;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.GetComponent<PlayerBehaviour>()) {
            BonusEffect();
            Destroy(this.gameObject);
        }
    }
}
