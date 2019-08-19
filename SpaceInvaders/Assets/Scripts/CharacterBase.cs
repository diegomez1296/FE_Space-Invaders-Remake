using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CharacterBase : MonoBehaviour
{
    public int HP { get; set; }

    public virtual void GetDamage(int damage) {
        HP -= damage;
        if (HP <= 0)
            Destroy(this.gameObject);
    }
}
