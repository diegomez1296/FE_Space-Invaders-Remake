using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponBoxController : MonoBehaviour
{
    private void FixedUpdate() {
        if (GetComponentsInChildren<BulletBase>().Length == 0)
            Destroy(this.gameObject);
    }
}
