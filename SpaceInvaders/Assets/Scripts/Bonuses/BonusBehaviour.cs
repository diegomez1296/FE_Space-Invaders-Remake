using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BonusBehaviour : MonoBehaviour
{
    public const int HP_LIMIT = 4;
    public const float SPEED_BULLET_LIMIT = 0.3f;
    public const int BULLETS_LIMIT = 5;

    [SerializeField]
    private PlayerBehaviour player;
    [SerializeField]
    private BonusType bonusType;
    public BonusType BonusType { get { return bonusType; } }
    [SerializeField]
    private GameObject bullet;

    private void FixedUpdate() {
        if(this.gameObject.activeSelf)
            this.gameObject.transform.Translate(new Vector3(0, 1, 0) * -0.05f);
    }
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
                var BulletsTab = bullet.transform.parent.GetComponentsInChildren<BulletBase>();
                if (BulletsTab.Length < BULLETS_LIMIT) {
                    Instantiate(bullet, bullet.transform.parent);
                    CheckNumberOfBullets(BulletsTab);
                }
                break;
            default:
                break;
        }
    }

    private void CheckNumberOfBullets(BulletBase[] BulletsTab) {

        BulletsTab = bullet.transform.parent.GetComponentsInChildren<BulletBase>();
        if(BulletsTab.Length % 2 !=0)
            bullet.transform.parent.transform.position = new Vector2(-BulletsTab.Length / 2, 0);
        else
            bullet.transform.parent.transform.position = new Vector2((-BulletsTab.Length / 2)+0.5f, 0);

        int x = 0;
        foreach (var item in BulletsTab) {
            item.transform.position = new Vector2(x, 0);
            x -= 1;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.GetComponent<PlayerBehaviour>()) {
            BonusEffect();
            Destroy(this.gameObject);
        }
    }
}
