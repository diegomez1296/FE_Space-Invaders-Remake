using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    private float maxX = 10.0f, minX = -10.0f, maxY = 7.0f, minY = -7.0f;

    [SerializeField]
    private float spaceshipSpeed;

    [SerializeField]
    private GameObject playerBullet;
    private Vector3 spaceshipPosition;

    // Start is called before the first frame update
    private void Start() {
        spaceshipPosition = this.gameObject.transform.position;
    }

    // Update is called once per frame
    private void FixedUpdate() {
        Moving();
        Shooting();
    }

    private void Moving() {
        spaceshipPosition.x += Input.GetAxis("Horizontal") * spaceshipSpeed;
        spaceshipPosition.y += Input.GetAxis("Vertical") * spaceshipSpeed;

        spaceshipPosition.x = Mathf.Clamp(spaceshipPosition.x, minX, maxX);
        spaceshipPosition.y = Mathf.Clamp(spaceshipPosition.y, minY, maxY);

        this.gameObject.transform.position = spaceshipPosition;
    }

    private void Shooting() {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)) {
            var copyBullet = Instantiate(playerBullet, spaceshipPosition, new Quaternion(0, 0, 180, 1));
            copyBullet.SetActive(true);
        }
    }


}
