using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class StatSlider : MonoBehaviour {
    [SerializeField]
    protected Slider slider;

    [SerializeField]
    protected TextMeshProUGUI textLabel;

    [SerializeField]
    protected TextMeshProUGUI textValue;

    [SerializeField]
    protected string label;

    [SerializeField]
    protected bool showAsPercent = false;


    public void OnValueChanged() {
        UpdateValueLabel();
    }

    private void UpdateValueLabel() {
        if (textValue == null || slider == null) { return; }

        if (showAsPercent) {
            int percent = (int)Math.Round(100 * slider.normalizedValue);
            StringBuilder sb = new StringBuilder();
            sb.Append(percent);
            sb.Append("%");
            textValue.SetText(sb.ToString());
        }
        else {
            textValue.SetText(slider.value.ToString());
        }
    }

#if UNITY_EDITOR
    private void Update() {
        if (textLabel != null) { textLabel.SetText(label); }
        UpdateValueLabel();
    }

#endif
}
