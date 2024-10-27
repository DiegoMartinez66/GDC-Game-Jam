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
         ChasePlayer enemy = other.GetComponent<ChasePlayer>();
         if (enemy != null)
         {
            other.GetComponent<ChasePlayer>().Stun(5000);
         }
    }
}
