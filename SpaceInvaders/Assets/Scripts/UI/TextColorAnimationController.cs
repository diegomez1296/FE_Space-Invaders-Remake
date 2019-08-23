using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TextColorAnimationController : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI text;
    [SerializeField]
    private float minValue = 180;
    [SerializeField]
    private float maxValue = 255;
    [SerializeField]
    private bool startFromMaxValue;
    private float currentValue;
    private bool isMax = false;
    [HideInInspector]
    public List<float> textColorValues;

    void Awake() {
        textColorValues = GetColorValues(text);
        if (startFromMaxValue)
            currentValue = maxValue;
        else
            currentValue = minValue;
    }

    void Update() {
        ChangeColor();
    }

    void ChangeColor() {
        if (!isMax) {
            text.color = new Color(textColorValues[0], textColorValues[1], textColorValues[2], currentValue / 255f);
            currentValue++;
            if (currentValue >= maxValue)
                isMax = true;
        }
        else if (isMax) {
            text.color = new Color(textColorValues[0], textColorValues[1], textColorValues[2], currentValue / 255f);
            currentValue--;
            if (currentValue <= minValue)
                isMax = false;
        }
    }

    private List<float> GetColorValues(TextMeshProUGUI text) {
        textColorValues.Add(text.color.r);
        textColorValues.Add(text.color.g);
        textColorValues.Add(text.color.b);
        return textColorValues;
    }
}
