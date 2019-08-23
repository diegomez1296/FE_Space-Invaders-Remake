using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossShieldBehaviour : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision) {
        if(this.gameObject.activeSelf) {
            if(collision.GetComponent<BulletBase>().IsPlayerBullet && !collision.GetComponent<BulletBase>().IsAimPlayerBullet) {  
                Destroy(collision.gameObject);
            }
        }
    }
}
