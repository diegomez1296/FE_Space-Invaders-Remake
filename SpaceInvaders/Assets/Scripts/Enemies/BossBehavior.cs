using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBehavior : EnemyBehaviour {

    [SerializeField]
    private PlayerBehaviour player;
    [SerializeField]
    private SpriteRenderer healthSprite;
    private int maxHP;

    protected override void Start() {
        base.Start();
        IsBoss = true;
    }

    protected override float GetRandomValue(RandOption option) {
        switch (option) {
            case RandOption.SHOT_TIME:
                float randValue = Random.Range(1.0f - (GameController.GameLevel * 0.2f), 6.0f - (GameController.GameLevel * 0.2f));
                return randValue >= 0.5f ? randValue : 0.5f;
                //if (GameController.GameLevel >= 16)
                //    return Random.Range(0.25f, 0.5f);
                //else if (GameController.GameLevel >= 8)
                //    return Random.Range(0.5f, 1.0f);
                //else
                //    return Random.Range(1.0f, 1.5f);

            case RandOption.BULLET_SPEED:
                return Random.Range(0, 0.05f);
                //if (GameController.GameLevel >= 16)
                //    return Random.Range(0.1f, 0.15f);
                //else if (GameController.GameLevel >= 8)
                //    return Random.Range(0.05f, 0.1f);
                //else
                //    return Random.Range(0, 0.05f);
            default:
                return 0.0f;
        }
    }

    protected override void Moving() {
        if (player != null) {
            this.gameObject.transform.Translate(new Vector3(3.0f, 0, 0) * enemySpeed);
            enemyPosition = this.gameObject.transform.position;
            if (!isRightDirection && enemyPosition.x < player.transform.position.x - 3 || isRightDirection && enemyPosition.x > player.transform.position.x + 3) {
                enemySpeed *= -1;
                isRightDirection = !isRightDirection;
            }
        }
    }

    protected override void Shooting() {

        if (actualTime <= 0) {
            var copyEnemyBullet = Instantiate(enemyBullet, enemyPosition, new Quaternion(0, 0, 0, 1));
            copyEnemyBullet.SetActive(true);
            foreach (var item in copyEnemyBullet.GetComponentsInChildren<BulletBase>()) {
                item.BulletSpeed -= GetRandomValue(RandOption.BULLET_SPEED);
                item.BulletSpeed -= (GameController.GameLevel * 0.005f);
            }
            ShootTime();
        }
        else
            actualTime -= Time.deltaTime;
    }

    public override void GetDamage(int damage, Vector2 bossPosition, int percentToExplosion) {
        base.GetDamage(damage, bossPosition, 100);

        float healthFract = HP / (float)maxHP;
        if (healthFract > 0.66f) {
            healthSprite.color = Color.green;
            return;
        }

        if (healthFract > 0.33f) {
            healthSprite.color = Color.yellow;
            return;
        }

        healthSprite.color = Color.red;
    }

    protected override void EnemyIsDead(Vector2 position) {
        bonusController.RandBonus(100, position);
    }

    public override void ActivateEnemy(bool isActive, bool isBoss) {
        base.ActivateEnemy(isActive, isBoss);
        maxHP = HP;
    }
}
