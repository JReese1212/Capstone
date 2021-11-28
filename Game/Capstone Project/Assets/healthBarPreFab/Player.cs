
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{

	public int maxHealth = 100;
	public int health;

	public HealthBar healthBar;

	void Start()
    {
		health = maxHealth;
		healthBar.SetMaxHealth(maxHealth);
    }



	private void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.tag == "Cop")
		{
			health -= 5;
			healthBar.SetHealth(health);
		}


	}

}
