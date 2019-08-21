using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WaveController : MonoBehaviour
{
    private TextMeshProUGUI waveValue;
    private int wave;

    private void Start() {
        wave = 0;
        waveValue = GetComponentsInChildren<TextMeshProUGUI>()[1];
        this.gameObject.SetActive(false);
    }

    public void ShowWaveText() {
        wave++;
        waveValue.text = wave + "";
        this.gameObject.SetActive(true);
    }

    public void HideWaveText() {
        this.gameObject.SetActive(false);
    }
}
