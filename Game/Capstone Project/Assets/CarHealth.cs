using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;

    public healthBarScript healthBar;

    void Start()
    {
        //healthBar = GameObject.Find("Health Bar");
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Cop")
        {
            currentHealth -= 25;
            healthBar.SetHealth(currentHealth);
        }


    }
}
