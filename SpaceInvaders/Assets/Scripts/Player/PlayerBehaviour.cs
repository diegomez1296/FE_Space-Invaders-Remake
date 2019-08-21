using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : CharacterBase {

    [SerializeField]
    private UIController ui;
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
                if(GameController.NickName != "")
                    AplicationController.SaveScore(new PlayerScore(GameController.NickName, GameController.GameLevel+"", ui.Score.score+""));
            }
            else {
                this.GetComponent<PlayerController>().SetStartPosition();
                StartCoroutine(ActivateShield(3.0f));
            }
        }
    }

    public void AddScore() {
        if(HP>0)
            ui.Score.AddScoreValue(GameController.GameLevel);
    }

    public void CheckLevelUI() {
        if (HP > 0)
            ui.Level.CheckUILevelValue();
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
