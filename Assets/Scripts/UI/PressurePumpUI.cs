using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PressurePumpUI : MonoBehaviour {

    [SerializeField] private Image pressurePumpImage;

    [SerializeField] private TextMeshProUGUI pumpPressureText;

    [SerializeField] InspectableObject InspectableObject;

    private float pressurePumpMaxValue = 100f;

    private float currentPressurePumpValue;

    private void Start() {
        currentPressurePumpValue = pressurePumpMaxValue;
    }

    private void Update() {

        SetPressurePumpValue();

    }

    private void SetPressurePumpValue() {

        float givenAmount = InspectableObject.GetScrollAmount() * 10f;

        currentPressurePumpValue += givenAmount;


        SetPressurePumpImageValue();
        SetPressurePumpVisual();

        if (currentPressurePumpValue >= pressurePumpMaxValue) {
            currentPressurePumpValue = pressurePumpMaxValue;
        }

        if (currentPressurePumpValue < 0f) {
            currentPressurePumpValue = 0f;
        }
    }

    private void SetPressurePumpImageValue() {

        pressurePumpImage.fillAmount = GetNormazliedPressurePumpValue();

    }

    private float GetNormazliedPressurePumpValue() {

        return currentPressurePumpValue / pressurePumpMaxValue;

    }

    private void SetPressurePumpVisual() {

        if (currentPressurePumpValue > 80f) {

            SetPressurePumpVisual(Color.red, "High");


        }
        else if (currentPressurePumpValue > 40f && currentPressurePumpValue < 80f) {

            SetPressurePumpVisual(Color.yellow, "Medium");

        }
        else if (currentPressurePumpValue > 0f && currentPressurePumpValue < 40f) {

            SetPressurePumpVisual(Color.green, "Low");

        }

    }

    private void SetPressurePumpVisual(Color color, string level) {

        pressurePumpImage.color = color;
        pumpPressureText.text = level;
        pumpPressureText.color = color;

    }

}