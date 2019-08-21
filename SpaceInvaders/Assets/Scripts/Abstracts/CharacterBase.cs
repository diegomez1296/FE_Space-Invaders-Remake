using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CharacterBase : MonoBehaviour
{
    [SerializeField]
    private GameObject explosionEffect;
    [HideInInspector]
    private Vector3 explosionOffset;
    public int HP { get; set; }

    public virtual void GetDamage(int damage) {
        HP -= damage;
        GameObject explosionFX;
        if (HP <= 0) {
            explosionFX = Instantiate(explosionEffect, transform.position + explosionOffset, Quaternion.identity) as GameObject;
            Destroy(this.gameObject);
            Destroy(explosionFX, 5.0f);
        }
        else {
            explosionFX = Instantiate(explosionEffect, transform.position + explosionOffset, Quaternion.identity, this.transform) as GameObject;
            Destroy(explosionFX, 5.0f);
        }
    }
}
