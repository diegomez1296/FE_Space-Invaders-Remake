using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static int GameLevel;
    public static int EnemyKills;

    //GL^2 * 5

    // Start is called before the first frame update
    void Start()
    {
        GameLevel = 1;
        EnemyKills = 10;
    }

    public static void AddEnemyKills() {
        EnemyKills += 1;
        Debug.Log(EnemyKills);

        if (GameLevel * GameLevel * 5 >= EnemyKills)
            GameLevel++;
    }

    public static int GetEnemyKills() {
        return EnemyKills;
    }
}
