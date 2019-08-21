using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

    public static int GameLevel;
    public static int EnemyKills;

    [SerializeField]
    private PlayerController playerController;
    [SerializeField]
    private GameObject enemy;
    [HideInInspector]
    public List<GameObject> listOfEnemy;
    [SerializeField]
    private WaveController waveController;

    //GL^2 * 50

    void Awake() {
        GameLevel = 1;
        EnemyKills = 0;
    }

    private void Start() {
        StartCoroutine(ActivateNewWave());
        InvokeRepeating("CheckEnemies", 5.0f, 5.0f);
    }

    public static void AddEnemyKills() {
        EnemyKills++;

        if (GameLevel * GameLevel * 5 <= EnemyKills)
            GameLevel++;
    }

    private void CheckEnemies() {

        bool time4NewWave = true;

        foreach (var item in listOfEnemy) {
            if (item != null) {
                time4NewWave = false;
                break;
            }
        }

        if(time4NewWave)
            SetNewWave();
    }
    private void SetNewWave()
    {
        StartCoroutine(ActivateNewWave());
    }

    private void InitEnemiesWave() {

        float x = -7;
        float y = 4;
        listOfEnemy.Clear();
        for (int j = 0; j < 4; j++) {

            for (int i = 0; i < 10; i++) {
                var enemyCopy = Instantiate(enemy, enemy.transform.parent);
                enemyCopy.transform.position = new Vector3(x, y, 0);

                x += 1.5f;
                listOfEnemy.Add(enemyCopy);
            }
            x = -7;
            y -= 1f;
        }
    }

    private void ActivateEnemies()
    {
        foreach (var item in listOfEnemy)
        {
            if (item != null)
            {
                item.GetComponent<EnemyBehaviour>().ActivateEnemy(true);
                item.gameObject.SetActive(true);
            }
        }
    }

    private IEnumerator ActivateNewWave()
    {
        playerController.IsShooting = false;
        waveController.ShowWaveText();
        InitEnemiesWave();
        yield return new WaitForSeconds(3f);
        waveController.HideWaveText();
        ActivateEnemies();
        playerController.IsShooting = true;

    }
}
