using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBehavior : EnemyBehaviour {

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
                if (GameController.GameLevel >= 10)
                    return Random.Range(0.25f, 0.5f);
                else if (GameController.GameLevel >= 5)
                    return Random.Range(0.5f, 1.0f);
                else
                    return Random.Range(1.0f, 1.5f);

            case RandOption.BULLET_SPEED:
                if (GameController.GameLevel >= 10)
                    return Random.Range(0.1f, 0.15f);
                else if (GameController.GameLevel >= 5)
                    return Random.Range(0.05f, 0.1f);
                else
                    return Random.Range(0, 0.05f);
            default:
                return 0.0f;
        }
    }

    protected override void Moving() {
        base.Moving();
        //https://youtu.be/rhoQd6IAtDo
    }

    protected override void Shooting() {

        if (actualTime <= 0) {
            var copyEnemyBullet = Instantiate(enemyBullet, enemyPosition, new Quaternion(0, 0, 0, 1));
            copyEnemyBullet.SetActive(true);
            foreach (var item in copyEnemyBullet.GetComponentsInChildren<BulletBase>()) {
                item.BulletSpeed -= GetRandomValue(RandOption.BULLET_SPEED);
                item.BulletSpeed -= (GameController.GameLevel * 0.01f);
            }
            ShootTime();
        }
        else
            actualTime -= Time.deltaTime;
    }

    public override void GetDamage(int damage) {
        base.GetDamage(damage);

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

    public override void ActivateEnemy(bool isActive, bool isBoss) {
        base.ActivateEnemy(isActive, isBoss);
        maxHP = HP;
    }
}
