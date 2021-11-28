using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class healthBarScript : MonoBehaviour
{
    public Slider slider;
    [SerializeField] private GameObject EndScreen;
    private EndScreenHandler EndScore;

    void Start()
    {
        EndScore = EndScreen.GetComponent(typeof(EndScreenHandler)) as EndScreenHandler;
    }

    void Update()
    {
 
        if(slider.value == 0)
        {
            Time.timeScale = 0f;
            
            EndScreen.SetActive(true);
            EndScore.DisplayScore();
            slider.value = -1;
        }

    }

    public void SetMaxHealth(int health)
    {
        slider.maxValue = health;
        slider.value = health;
    }

    public void SetHealth(int health)
    {
        slider.value = health;
    }
}
