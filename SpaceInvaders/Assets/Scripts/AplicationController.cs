using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AplicationController : MonoBehaviour
{
    public void LoadScene(int sceneIndex) 
    {
        switch (sceneIndex)
        {
            case 0:
                SceneManager.LoadScene("Menu");
                break;
            case 1:
                SceneManager.LoadScene("Game");
                break;
        }    
    }

    public void ExitButtonClick() 
    {
        Application.Quit();
    }
}
