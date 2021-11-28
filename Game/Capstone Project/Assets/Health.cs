using UnityEngine;



public class Health : MonoBehaviour
{

   // public int maxHealth = 100;
   // public int currentHealth;
    //public HealthBar healthBar;

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Car")
        {

            Debug.Log("cop hit car");
            //currentHealth -= 5;
            //healthBar.SetHealth(currentHealth);
        }

        
    }
}
