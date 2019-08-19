using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : CharacterBase {

    [SerializeField]
    private PlayerLifesController playerLifes;

    private void Start() {
        HP = 3;
    }

    public override void GetDamage(int damage) {
        base.GetDamage(damage);
        playerLifes.CheckPlayerLifes(HP);
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.GetComponent<EnemyBehaviour>()) {
            Destroy(collision.gameObject);
            GetDamage(1);
        }
    }
}
