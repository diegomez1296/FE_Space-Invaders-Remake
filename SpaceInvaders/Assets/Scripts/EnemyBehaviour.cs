using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyBehaviour : MonoBehaviour {

    [SerializeField]
    private bool isShooting;
    [SerializeField]
    private bool isMoving;
    [SerializeField]
    private float enemySpeed;
    [SerializeField]
    private GameObject enemyBullet;

    private Vector3 enemyPosition;
    private float actualTime;

    // Start is called before the first frame update
    private void Start() {
        actualTime = ShootTime();
    }

    private void FixedUpdate() {
        if (isMoving) Moving();
        if (isShooting) Shooting();
    }

    private void Shooting() {

        if (actualTime <= 0) {
            var copyEnemyBullet = Instantiate(enemyBullet, enemyPosition, new Quaternion(0, 0, 0, 1));
            copyEnemyBullet.SetActive(true);
            ShootTime();
        }
        else {
            actualTime -= Time.deltaTime;
        }

    }

    private void Moving() {
        this.gameObject.transform.Translate(new Vector3(3.0f, 0, 0) * enemySpeed);
        enemyPosition = this.gameObject.transform.position;
        if (enemyPosition.x > 10 || enemyPosition.x < -10) {
            enemySpeed *= -1;
        }
    }

    private float ShootTime() {
        float shootTime = Random.Range(1, 5);
        actualTime = shootTime;
        return shootTime;
    }



}
