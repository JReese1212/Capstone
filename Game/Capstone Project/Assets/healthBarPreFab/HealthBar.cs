using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider healthBar;

    public void SetMaxHealth(int Max)
    {
        healthBar.maxValue = Max;
        healthBar.value = Max;
    }

    public void SetHealth(int hp)
    {
        healthBar.value = hp;
    }
}