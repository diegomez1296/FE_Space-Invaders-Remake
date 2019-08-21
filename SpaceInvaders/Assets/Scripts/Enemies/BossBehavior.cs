using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBehavior : EnemyBehaviour {

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
}
