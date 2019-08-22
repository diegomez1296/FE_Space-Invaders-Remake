using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFormationController : MonoBehaviour
{
    public enum EnemyFormations { F4_10, F5_8 }

    private GameObject enemy;
    private List<GameObject> listOfEnemy;
    private bool[] wasChangeFormation;

    public EnemyFormationController(GameObject enemy, List<GameObject> listOfEnemy, bool[] wasChangeFormation) {
        this.enemy = enemy;
        this.listOfEnemy = listOfEnemy;
        this.wasChangeFormation = wasChangeFormation;
    }

    public void SetStartFormation(int rowNumber, int columnNumber, float startX, float startY, float deltaX, float deltaY) {
        float x = startX;
        float y = startY;

        for (int j = 0; j < rowNumber; j++) {
            for (int i = 0; i < columnNumber; i++) {
                var enemyCopy = Instantiate(enemy, enemy.transform.parent);
                enemyCopy.transform.position = new Vector3(x, y, 0);
                listOfEnemy.Add(enemyCopy);

                x += deltaX;
            }
            x = startX;
            y += deltaY;
        }
    }

    public void CheckFormations(int[] formationDestinations, float startX, float startY, float deltaX, float deltaY) {

        CheckSingleFormation(0, formationDestinations[0], startX, startY, deltaX, deltaY);
        CheckSingleFormation(1, formationDestinations[1], startX, startY, deltaX, deltaY);
        CheckSingleFormation(2, formationDestinations[2], startX, startY, deltaX, deltaY);
    }

    private void CheckSingleFormation(int formationChangeNumber, int enemyBorder, float startX, float startY, float deltaX, float deltaY) {

        if (!wasChangeFormation[formationChangeNumber]) {

            int actualEnemies = 0;
            foreach (var item in listOfEnemy)
                if (item != null)
                    actualEnemies++;

            if (actualEnemies <= enemyBorder)
                FixFormation(formationChangeNumber, startX, startY, deltaX, deltaY);
        }
    }

    private void FixFormation(int formationChangeNumber, float startX, float startY, float deltaX, float deltaY) {

        float x = startX;
        float y = startY;
        int enemyInRow = 0;

        foreach (var item in listOfEnemy) {
            if (item != null) {
                item.transform.position = new Vector3(x, y, 0);
                x += deltaX;
                enemyInRow++;
                if (enemyInRow > 9) {
                    y += deltaY;
                    x = startX;
                    enemyInRow = 0;
                }
            }
        }
        wasChangeFormation[formationChangeNumber] = true;
    }

}
