using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyBehaviour : CharacterBase {

    protected enum RandOption { SHOT_TIME, BULLET_SPEED}

    public bool IsBoss { get; set; }

    [HideInInspector]
    protected bool isMoving;
    [HideInInspector]
    protected bool isMortal;
    [HideInInspector]
    protected bool isShooting;
    [SerializeField]
    protected float enemySpeed;
    [SerializeField]
    protected GameObject enemyBullet;
    [SerializeField]
    protected Slider slider;
    protected bool isRightDirection;
    protected Vector3 enemyPosition;
    protected float actualTime;

    private float maxPositionX;
    private float minPositionX;

    protected virtual void Start() {
        actualTime = ShootTime();
        IsBoss = false;
        isRightDirection = true;
        maxPositionX = GameController.ResMaxX + 0.34f;
        minPositionX = GameController.ResMinX - 0.34f;
    }

    protected void FixedUpdate() {
        if (isMoving) Moving();
        if (isShooting) Shooting();
    }

    public override void GetDamage(int damage, Vector2 enemyPosition, int percentToExplosion) {
        if (isMortal) {
            base.GetDamage(damage, enemyPosition, percentToExplosion);
            if (HP <= 0) {
                GameController.AddEnemyKills();
                slider.value++;
            }
        }
    }

    protected virtual void Shooting() {

        if (actualTime <= 0) {
            var copyEnemyBullet = Instantiate(enemyBullet, enemyPosition, new Quaternion(0, 0, 0, 1));
            copyEnemyBullet.SetActive(true);
            copyEnemyBullet.GetComponent<BulletBase>().bulletSpeed -= GetRandomValue(RandOption.BULLET_SPEED);
            copyEnemyBullet.GetComponent<BulletBase>().bulletSpeed -= (GameController.GameLevel * 0.005f);
            ShootTime();
        }
        else
            actualTime -= Time.deltaTime;
    }

    protected virtual void Moving() {
        this.gameObject.transform.Translate(new Vector3(3.0f, 0, 0) * enemySpeed);
        enemyPosition = this.gameObject.transform.position;
        if (enemyPosition.x > maxPositionX || enemyPosition.x < minPositionX) {
            enemySpeed *= -1;
            isRightDirection = !isRightDirection;
        }
    }

    protected float ShootTime() {
        float shootTime = GetRandomValue(RandOption.SHOT_TIME);
        actualTime = shootTime;
        return shootTime;
    }

    protected virtual float GetRandomValue(RandOption option)
    {
        switch (option)
        {
            case RandOption.SHOT_TIME:
                float randValue = Random.Range(2.0f - (GameController.GameLevel * 0.1f), 6.0f - (GameController.GameLevel * 0.1f));
                return  randValue >= 1 ? randValue : 1;
                //if (GameController.GameLevel >= 10)
                //    return Random.Range(1.0f, 3.0f);
                //else if (GameController.GameLevel >= 5)
                //    return Random.Range(2.0f, 6.0f);
                //else
                //    return Random.Range(3.0f, 9.0f);

            case RandOption.BULLET_SPEED:
                return Random.Range(-0.025f, 0.025f);
                //if (GameController.GameLevel >= 10)
                //    return Random.Range(0.05f, 0.1f);
                //else if (GameController.GameLevel >= 5)
                //    return Random.Range(0, 0.05f);
                //else
                //    return Random.Range(-0.025f, 0.025f);
            default:
                return 0.0f;
        }     
    }

    public virtual void ActivateEnemy(bool isActive, bool isBoss)
    {
        isMoving = isActive;
        isMortal = isActive;
        isShooting = isActive;

        if (isBoss)
            HP = GameController.GameLevel + GameController.GameLevel;
        else
            HP = (GameController.GameLevel / 5) + 1;
    }

    protected override void EnemyIsDead(Vector2 position) {
        bonusController.RandBonus(10, position);
    }
}
