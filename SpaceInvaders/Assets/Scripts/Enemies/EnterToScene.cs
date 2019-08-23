using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterToScene : MonoBehaviour
{
    private bool isMoving;
    public void GoToScene()
    {
        this.transform.position = new Vector3(0, 5.0f);
        isMoving = true;        
    }

    private void FixedUpdate() {
        if (isMoving) {
            this.transform.Translate(Vector2.down * 0.1f);
            if (transform.position.y <= 0.0f)
                isMoving = false;
        }
    }
}
