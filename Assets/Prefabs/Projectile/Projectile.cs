using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private float speed;
    // Update is called once per frame
    void Update()
    {
        Destroy(gameObject, 5f);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Fish")) 
        {
            Destroy(gameObject);
        }
         Critter enemy = other.GetComponent<Critter>();
         if (enemy != null)
         {
            other.GetComponent<Critter>().Stun(5);
         }
    }
}
