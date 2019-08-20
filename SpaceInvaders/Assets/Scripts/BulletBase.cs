using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBase : MonoBehaviour {

    [SerializeField]
    private PlayerBehaviour player;

    [SerializeField]
    private float bulletSpeed;
    [SerializeField]
    private int bulletDamage;
    [SerializeField]
    private bool isPlayerBullet;
    public bool IsPlayerBullet { get { return isPlayerBullet; } }

    private void FixedUpdate() {
        if(this.gameObject.activeSelf) 
        {
            Moving();
            DestroyMoment();
        }
    }

    private void Moving() 
    {
        this.gameObject.transform.Translate(new Vector3(0, 1, 0) * bulletSpeed);
    }

    private void DestroyMoment() 
    {
        if (this.gameObject.transform.position.y > 10 || this.gameObject.transform.position.y < -10)
            Destroy(this.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (this.gameObject.activeSelf) {
            if (collision.GetComponent<PlayerController>() && !IsPlayerBullet) {
                player.GetDamage(bulletDamage);
                Destroy(this.gameObject);
            }

            if (collision.GetComponent<EnemyBehaviour>() && IsPlayerBullet) {
                var enemy = collision.GetComponent<EnemyBehaviour>();
                enemy.GetDamage(bulletDamage);
                Destroy(this.gameObject);
                player.AddScore();
            }
        }
    }
}