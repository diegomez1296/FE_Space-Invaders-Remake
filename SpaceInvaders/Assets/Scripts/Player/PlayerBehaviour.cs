using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : CharacterBase {

    [SerializeField]
    private UIController ui;
    [SerializeField]
    private GameController gameController;
    [SerializeField]
    private GameObject shield;

    private void Start() {
        HP = 3;
    }


    public override void GetDamage(int damage) {
        if (!shield.activeSelf) {
            base.GetDamage(damage);
            ui.PlayerLifes.CheckPlayerLifes(HP);
            if (HP <= 0) {
                ui.ActivateGameOverText();
                Debug.Log("Enemy killed: " + GameController.EnemyKills);
            }
            else {
                this.GetComponent<PlayerController>().SetStartPosition();
                StartCoroutine(ActivateShield(3.0f));
            }
        }
    }
    //GameObject explosionFX = Instantiate(explosionEffect, transform.position, Quaternion.identity) as GameObject;
    //Destroy(explosionFX, 5.0f);
    //explosionEffect.Play();

    public void AddScore() {
        ui.Score.AddScoreValue(GameController.GameLevel);
    }


    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.GetComponent<EnemyBehaviour>()) {
            shield.SetActive(false);
            collision.GetComponent<EnemyBehaviour>().GetDamage(1);
            GetDamage(1);
        }
    }

    private IEnumerator ActivateShield(float shieldTime) {
        shield.SetActive(true);
        yield return new WaitForSeconds(shieldTime);
        shield.SetActive(false);
    }
}
