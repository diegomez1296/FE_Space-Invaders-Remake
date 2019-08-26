using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    [SerializeField]
    private GameObject playerBullet;
    [SerializeField]
    private GameObject playerRocket;
    [SerializeField]
    private float spaceshipSpeed;

    public bool IsShooting { get; set; }
    public bool IsMoving { get; set; }
    public float SpeedBulletMod { get; set; }
    public int RocketAmount { get; set; }

    private float maxPositionX;
    private float minPositionX;
    private float minPositionY;
    private float maxPositionY;

    private Vector3 playerBulletsStartPos;
    private Vector3 spaceshipPosition;
    private Vector3 startPosition;

    private void Start() {
        IsMoving = true;
        IsShooting = true;
        SpeedBulletMod = 0;
        spaceshipPosition = this.gameObject.transform.position;
        startPosition = spaceshipPosition;
        playerBulletsStartPos = playerBullet.transform.position;
        RocketAmount = 0;

        maxPositionX = GameController.ResMaxX - 0.34f;
        minPositionX = GameController.ResMinX + 0.34f;
        maxPositionY = GameController.ResMaxY;
        minPositionY = GameController.ResMinY;
    }

    private void FixedUpdate()
    {
        if (IsMoving) Moving();

    }
    private void Update()
    {
        if (IsShooting) { Shooting(); EnableRocket(); }
    }

    private void Moving() {
        if (!ApplicationController.isMouseControl) {
            spaceshipPosition.x += Input.GetAxis("Horizontal") * spaceshipSpeed;
            spaceshipPosition.y += Input.GetAxis("Vertical") * spaceshipSpeed;
        }
        else
            spaceshipPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        spaceshipPosition.x = Mathf.Clamp(spaceshipPosition.x, minPositionX, maxPositionX);
        spaceshipPosition.y = Mathf.Clamp(spaceshipPosition.y, minPositionY, maxPositionY);
        spaceshipPosition.z = 0;

        this.gameObject.transform.position = spaceshipPosition;
    }

    private void Shooting() {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)) {
            var copyBullet = Instantiate(playerBullet, spaceshipPosition, new Quaternion(0, 0, 180, 1));
            copyBullet.SetActive(true);

            foreach (var item in playerBullet.GetComponentsInChildren<BulletBase>()) {
                item.bulletSpeed = SpeedBulletMod * -1 - 0.1f;
            }      
        }
    }

    private void EnableRocket() {
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetMouseButtonDown(1)) {
            if (RocketAmount > 0) {
                var copyRacket = Instantiate(playerRocket, spaceshipPosition, new Quaternion(0, 0, 180, 1));
                copyRacket.SetActive(true);
                copyRacket.GetComponent<BulletBase>().bulletSpeed = -0.1f;
                RocketAmount--;
                GetComponent<PlayerBehaviour>().ui.Rocket.UpdateRocketValue(RocketAmount);
            }
        }
    }

    public void DestroyBullets() {
        BulletBase[] bullets = playerBullet.GetComponentsInChildren<BulletBase>();
        for (int i = 1; i < bullets.Length; i++) {
            Destroy(bullets[i].gameObject);
        }

        playerBullet.transform.position = playerBulletsStartPos;
        foreach (var item in bullets) {
            item.transform.position = Vector3.zero;
        }
        //
        SpeedBulletMod = 0;
    }

    public void SetStartPosition() {
        spaceshipPosition = startPosition;
        this.gameObject.transform.position = startPosition;
    }
}
