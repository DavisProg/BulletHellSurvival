using System;
using UnityEngine;
using System.Collections;
using Unity.VisualScripting;


public class Pickup : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log("pp");
        if (collision.CompareTag("Player"))
        {
            Destroy(gameObject);

            if (collision.TryGetComponent(out Player component))
	        {
		        component.increaseXP(1);
	        }
        }
    }
}

