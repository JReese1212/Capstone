using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectDestructionPhysics : MonoBehaviour
{
    [SerializeField] private int ObjectMass = 1;
     private Rigidbody myObj;
     private bool IsHit = false;


    private void OnTriggerEnter(Collider other)
    {
        if (!IsHit)
        {
            IsHit = true;
            myObj = gameObject.AddComponent<Rigidbody>();
            myObj.mass = ObjectMass;


            Object.Destroy(gameObject, 20.0f);
        }
    }
}
