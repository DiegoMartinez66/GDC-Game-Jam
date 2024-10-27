using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed;
    public int damage = 1; // Amount of damage the projectile deals
    public bool isCritterBullet;

    // Update is called once per frame
    void Update()
    {
        Destroy(gameObject, 5f);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!isCritterBullet)
        {
            if (other.CompareTag("Fish"))
            {
                Destroy(gameObject);
            }

            Critter enemy = other.GetComponent<Critter>();
            if (enemy != null)
            {
                enemy.Stun(5);
                Destroy(gameObject);
            }

            BossCritter boss = other.GetComponent<BossCritter>();
            if (boss != null)
            {
                boss.TakeDamage(damage); // Apply damage to the BossCritter
                Destroy(gameObject); // Destroy the projectile upon collision
            }
        }
        else
        {
            if (other.CompareTag("Player"))
            {
                Player.Instance.TakeDamage(damage);
                Destroy(gameObject);
            }
        }
    }
}
