using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RocketController : MonoBehaviour
{
    private TextMeshProUGUI value;

    void Start()
    {
        value = GetComponentInChildren<TextMeshProUGUI>();
    }

    public void UpdateRocketValue(int actualRocketAmount) {
        value.text = actualRocketAmount+"";
    }

}
