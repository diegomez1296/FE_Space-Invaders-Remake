using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : CharacterBase {

    public UIController ui;
    [SerializeField]
    private GameObject shield;
    private int rocketPoints;

    private void Start() {
        HP = 3;
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.F12))
            shield.SetActive(!shield.activeSelf);
    }

    public override void GetDamage(int damage, Vector2 playerPosition, int percentToExplosion) {
        if (!shield.activeSelf) {
            base.GetDamage(damage, Vector2.zero, 100);
            ui.PlayerLifes.CheckPlayerLifes(HP);
            GetComponent<PlayerController>().DestroyBullets();
            if (HP <= 0) {
                ui.ActivateGameOverText();
                GameController.GameIsRunning = false;
                ApplicationController.canOpenHighScorePanel = true;
                ApplicationController.SaveScore(new PlayerScore(GameController.NickName, GameController.GameLevel+"", ui.Score.score+""));
            }
            else {
                this.GetComponent<PlayerController>().SetStartPosition();
                ActivateShield(3.0f);
            }
        }
    }

    public void AddScore(bool isBoss) {
        if (HP > 0) {
            if (!isBoss) {
                ui.Score.AddScoreValue(GameController.GameLevel);
                rocketPoints += GameController.GameLevel;
            }
            else {
                ui.Score.AddScoreValue(GameController.GameLevel * 100);
                rocketPoints += GameController.GameLevel * 100;
            }

            if(rocketPoints >= 1000) {
                rocketPoints -= 1000;
                GetComponent<PlayerController>().RocketAmount += 1;
                ui.Rocket.UpdateRocketValue(GetComponent<PlayerController>().RocketAmount);
            }
        }
    }

    public void CheckLevelUI() {
        if (HP > 0)
            ui.Level.CheckUILevelValue();
    }

    public void ActivateShield(float shieldTime) {
        StartCoroutine(CoActivateShield(shieldTime));
    }

    public void PlayBonusSound() {
        this.GetComponent<AudioSource>().Play();
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (!shield.activeSelf) {
            if (collision.GetComponent<EnemyBehaviour>()) {
                //shield.SetActive(false);
                collision.GetComponent<EnemyBehaviour>().GetDamage(1, Vector2.zero, 100);
                GetDamage(1, Vector2.zero, 100);
            }
        }
    }

    private IEnumerator CoActivateShield(float shieldTime) {
        shield.SetActive(true);
        yield return new WaitForSeconds(shieldTime);
        shield.SetActive(false);
    }
}
