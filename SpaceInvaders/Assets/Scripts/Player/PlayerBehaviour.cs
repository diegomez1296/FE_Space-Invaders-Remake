using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : CharacterBase {

    public UIController ui;
    [SerializeField]
    private GameObject shield;

    public Vector2 offset1;
    public Vector2 offset2;

    private void Start() {
        HP = 3;
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.F12))
            shield.SetActive(!shield.activeSelf);
    }

    public override void GetDamage(int damage, Vector2 playerPosition) {
        if (!shield.activeSelf) {
            base.GetDamage(damage, Vector2.zero);
            ui.PlayerLifes.CheckPlayerLifes(HP);
            GetComponent<PlayerController>().DestroyBullets();
            if (HP <= 0) {
                ui.ActivateGameOverText();
                GameController.GameIsRunning = false;
                if (GameController.NickName != "")
                    AplicationController.SaveScore(new PlayerScore(GameController.NickName, GameController.GameLevel+"", ui.Score.score+""));
            }
            else {
                this.GetComponent<PlayerController>().SetStartPosition();
                StartCoroutine(ActivateShield(3.0f));
            }
        }
    }

    public void AddScore(bool isBoss) {
        if (HP > 0) {
            if (!isBoss)
                ui.Score.AddScoreValue(GameController.GameLevel);
            else
                ui.Score.AddScoreValue(GameController.GameLevel*100);
        }
    }

    public void CheckLevelUI() {
        if (HP > 0)
            ui.Level.CheckUILevelValue();
    }


    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.GetComponent<EnemyBehaviour>()) {
            shield.SetActive(false);
            collision.GetComponent<EnemyBehaviour>().GetDamage(1, Vector2.zero);
            GetDamage(1, Vector2.zero);
        }
    }

    private IEnumerator ActivateShield(float shieldTime) {
        shield.SetActive(true);
        yield return new WaitForSeconds(shieldTime);
        shield.SetActive(false);
    }
}
