using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AplicationController : MonoBehaviour
{
    public static bool isMouseControl = false;
    [SerializeField]
    private Image controlButtonImage;
    [SerializeField]
    private Sprite mouseIcon;
    [SerializeField]
    private Sprite keyboardIcon;

    private void FixedUpdate() {
        if (Input.GetKeyDown(KeyCode.Escape))
            LoadScene(0);
    }

    public void LoadScene(int sceneIndex) 
    {
        switch (sceneIndex)
        {
            case 0:
                SceneManager.LoadScene("Menu");
                Cursor.visible = true;
                break;
            case 1:
                SceneManager.LoadScene("Game");
                Cursor.visible = false;
                break;
        }    
    }

    public void ExitButtonClick() 
    {
        Application.Quit();
    }

    public void ChangeControl() {
        isMouseControl = !isMouseControl;
        if (isMouseControl)
            controlButtonImage.sprite = mouseIcon;
        else
            controlButtonImage.sprite = keyboardIcon;
    }
}
