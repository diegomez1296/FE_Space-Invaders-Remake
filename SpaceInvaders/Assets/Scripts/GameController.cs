using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

    public static int GameLevel;
    public static int EnemyKills;

    [SerializeField]
    private GameObject enemy;
    [HideInInspector]
    public List<GameObject> listOfEnemy;
    [SerializeField]
    private WaveController waveController;

    //GL^2 * 5

    // Start is called before the first frame update
    void Awake() {
        GameLevel = 1;
        EnemyKills = 0;
    }

    private void Start() {
        waveController.ShowWaveText();
        StartWave();
    }

    public static void AddEnemyKills() {
        EnemyKills++;

        if (GameLevel * GameLevel * 5 <= EnemyKills)
            GameLevel++;
    }

    private void FixedUpdate() {

        bool isNewWave = true;

        foreach (var item in listOfEnemy) {
            if (item != null) {
                isNewWave = false;
                break;
            }
        }

        if(isNewWave) {
            listOfEnemy.Clear();
            waveController.ShowWaveText();
            StartWave();
        }

    }


    public void StartWave() {

        float x = -7;
        float y = 4;
        for (int j = 0; j < 4; j++) {

            for (int i = 0; i < 10; i++) {
                var enemyCopy = Instantiate(enemy, enemy.transform.parent);
                enemyCopy.transform.position = new Vector3(x, y, 0);
                enemyCopy.SetActive(true);

                x += 1.5f;
                listOfEnemy.Add(enemyCopy);
            }
            x = -7;
            y -= 1f;
        }
    }
}
