using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WaveController : MonoBehaviour
{
    private TextMeshProUGUI waveValue;
    public int Wave { get; set; }

    private void Start() {
        Wave = 0;
        waveValue = GetComponentsInChildren<TextMeshProUGUI>()[1];
        this.gameObject.SetActive(false);
    }

    public void ShowWaveText() {
        Wave++;
        waveValue.text = Wave + "";
        this.gameObject.SetActive(true);
    }

    public void HideWaveText() {
        this.gameObject.SetActive(false);
    }
}
