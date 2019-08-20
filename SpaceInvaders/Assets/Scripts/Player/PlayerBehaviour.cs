using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : CharacterBase {

    [SerializeField]
    private UIController ui;

    private void Start() {
        HP = 3;
    }


    public override void GetDamage(int damage) {
        base.GetDamage(damage);
        ui.PlayerLifes.CheckPlayerLifes(HP);
        if (HP <= 0)
            ui.ActivateGameOverText();
    }
    //GameObject explosionFX = Instantiate(explosionEffect, transform.position, Quaternion.identity) as GameObject;
    //Destroy(explosionFX, 5.0f);
    //explosionEffect.Play();

    public void AddScore(int newScore) {
        ui.Score.AddScoreValue(newScore);
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.GetComponent<EnemyBehaviour>()) {
            Destroy(collision.gameObject);
            GetDamage(1);
        }
    }
}
