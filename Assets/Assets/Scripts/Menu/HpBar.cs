using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpBar : MonoBehaviour
{
    [SerializeField] Slider slider;
    [SerializeField] TMPro.TextMeshProUGUI hpText;


    public void UpdateHpSlider(int current, int target)
    {
        slider.maxValue = target;
        slider.value = current;
        hpText.text = "HP: " + current.ToString() + "/" + target.ToString();
    }
}
