using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyBehaviour : CharacterBase {

    private enum RandOption { SHOT_TIME, BULLET_SPEED}

    [SerializeField]
    private bool isMoving;
    [SerializeField]
    private bool isMortal;
    [SerializeField]
    private bool isShooting;
    [SerializeField]
    private float enemySpeed;
    [SerializeField]
    private GameObject enemyBullet;
    [SerializeField]
    private Slider slider;

    private Vector3 enemyPosition;
    private float actualTime;

    // Start is called before the first frame update
    private void Start() {
        actualTime = ShootTime();
        HP = 1;
    }

    private void FixedUpdate() {
        if (isMoving) Moving();
        if (isShooting) Shooting();
    }

    public override void GetDamage(int damage) {
        if (isMortal)
        {
            base.GetDamage(damage);
            if (HP <= 0)
            {
                GameController.AddEnemyKills();
                slider.value++;
            }
        
        }
    }

    private void Shooting() {

        if (actualTime <= 0) {
            var copyEnemyBullet = Instantiate(enemyBullet, enemyPosition, new Quaternion(0, 0, 0, 1));
            copyEnemyBullet.SetActive(true);
            copyEnemyBullet.GetComponent<BulletBase>().BulletSpeed -= GetRandomValue(RandOption.BULLET_SPEED);
            copyEnemyBullet.GetComponent<BulletBase>().BulletSpeed -= (GameController.GameLevel * 0.01f);
            ShootTime();
        }
        else
            actualTime -= Time.deltaTime;
    }

    private void Moving() {
        this.gameObject.transform.Translate(new Vector3(3.0f, 0, 0) * enemySpeed);
        enemyPosition = this.gameObject.transform.position;
        if (enemyPosition.x > 10 || enemyPosition.x < -10)
            enemySpeed *= -1;
    }

    private float ShootTime() {
        float shootTime = GetRandomValue(RandOption.SHOT_TIME);
        actualTime = shootTime;
        return shootTime;
    }

    private float GetRandomValue(RandOption option)
    {
        switch (option)
        {
            case RandOption.SHOT_TIME:
                if (GameController.GameLevel >= 10)
                    return Random.Range(1.0f, 3.0f);
                else if (GameController.GameLevel >= 5)
                    return Random.Range(2.0f, 6.0f);
                else
                    return Random.Range(3.0f, 9.0f);

            case RandOption.BULLET_SPEED:
                if (GameController.GameLevel >= 10)
                    return Random.Range(0.05f, 0.1f);
                else if (GameController.GameLevel >= 5)
                    return Random.Range(0, 0.05f);
                else
                    return Random.Range(-0.025f, 0.025f);
            default:
                return 0.0f;
        }     
    }

    public void ActivateEnemy(bool isActive)
    {
        isMoving = isActive;
        isMortal = isActive;
        isShooting = isActive;
    }

}
