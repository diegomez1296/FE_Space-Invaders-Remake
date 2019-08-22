using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

    public static string NickName;
    public static int GameLevel;
    public static int EnemyKills;

    [SerializeField]
    private PlayerController playerController;
    [SerializeField]
    private GameObject enemy;
    [SerializeField]
    private GameObject boss;
    [HideInInspector]
    public List<GameObject> listOfEnemy;
    [SerializeField]
    private WaveController waveController;
    //EnemyFormations
    private EnemyFormationController enemyFormationController;
    private bool[] wasChangeFormation;

    void Awake() {
        GameLevel = 1;
        EnemyKills = 0;
    }

    private void Start() {
        StartCoroutine(ActivateNewWave(waveController.Wave+1)); //Wave+1
        InvokeRepeating("CheckEnemies", 5.0f, 5.0f);
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Escape))
            SceneManager.LoadScene("Menu");

        enemyFormationController.CheckFormations(new int[] { 10, 20, 30}, -7, 4, 1.5f, -1);
    }

    public static void AddEnemyKills() {
        EnemyKills++;

        if (GameLevel * GameLevel * 5 <= EnemyKills)
            GameLevel++;
    }

    private void CheckEnemies() {

        bool time4NewWave = true;

        foreach (var item in listOfEnemy)
            if (item != null) {
                time4NewWave = false;
                break;
            }

        if(time4NewWave)
            SetNewWave();
    }
    private void SetNewWave()
    {
        StartCoroutine(ActivateNewWave(waveController.Wave+1));
    }

    private void InitEnemiesWave(EnemyFormations formations) {

        listOfEnemy.Clear();

        switch (formations) {
            case EnemyFormations.F4_10:
                enemyFormationController.SetStartFormation(4, 10, -7, 4, 1.5f, -1);
                break;
            case EnemyFormations.F5_8:
                enemyFormationController.SetStartFormation(5, 8, -7, 4, 1.5f, -1);
                break;
        }
    }

    private EnemyFormations RandStartFormation() {

        int randValue = Random.Range(0, 2);
        switch (randValue) {
            case 0:
                return EnemyFormations.F4_10;
            case 1:
                return EnemyFormations.F5_8;
            default:
                return EnemyFormations.F4_10;
        }
    }

    private void InitBoss() {

        float x = -7;
        float y = 3;
        listOfEnemy.Clear();
        var bossCopy = Instantiate(boss, boss.transform.parent);
        bossCopy.transform.position = new Vector3(x, y, 0);
        listOfEnemy.Add(bossCopy);
    }

    private void ActivateEnemies(bool isBoss)
    {
        foreach (var item in listOfEnemy)
        {
            if (item != null)
            {
                if (!isBoss)
                    item.GetComponent<EnemyBehaviour>().ActivateEnemy(true, false);
                else
                    item.GetComponent<BossBehavior>().ActivateEnemy(true, true);
                item.gameObject.SetActive(true);
            }
        }
    }

    private IEnumerator ActivateNewWave(int waveNumber)
    {
        wasChangeFormation = new bool[3];
        enemyFormationController = new EnemyFormationController(enemy, listOfEnemy, wasChangeFormation);
        playerController.IsShooting = false;
        waveController.ShowWaveText();
        if (waveNumber % 3 != 0) InitEnemiesWave(RandStartFormation());
        else InitBoss();
        yield return new WaitForSeconds(3f);
        waveController.HideWaveText();
        if (waveNumber % 3 != 0) ActivateEnemies(false);
        else ActivateEnemies(true);
        playerController.IsShooting = true;

    }
}
