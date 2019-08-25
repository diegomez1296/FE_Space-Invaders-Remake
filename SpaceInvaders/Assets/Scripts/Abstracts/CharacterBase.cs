using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CharacterBase : MonoBehaviour
{
    [SerializeField]
    private GameObject explosionEffect;
    [SerializeField]
    protected BonusController bonusController;
    [HideInInspector]
    private Vector3 explosionOffset;
    public int HP { get; set; }

    public virtual void GetDamage(int damage, Vector2 position, int percentToExplosion) {
        HP -= damage;
        GameObject explosionFX;
        if (HP <= 0) {
            int rand = Random.Range(0, 100);
            if (rand <= percentToExplosion) {
                explosionFX = Instantiate(explosionEffect, transform.position + explosionOffset, Quaternion.identity) as GameObject;
                Destroy(explosionFX, 5.0f);
            }
            Destroy(this.gameObject);
            EnemyIsDead(position);
        }
        else {
            explosionFX = Instantiate(explosionEffect, transform.position + explosionOffset, Quaternion.identity, this.transform) as GameObject;
            Destroy(explosionFX, 5.0f);
        }
    }

    protected virtual void EnemyIsDead(Vector2 position) { }
}
