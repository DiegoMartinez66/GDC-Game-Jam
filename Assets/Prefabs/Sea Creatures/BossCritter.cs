using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossCritter : MonoBehaviour
{
    public AudioSource bossAudio;
    static public BossCritter Instance;
    public Rigidbody2D rb;
    public SpriteRenderer spriteRenderer;
    public float speed;
    public float bulletSpeed = 15;
    public float numberOfBulletsInBarrage;
    public float lookRadius;
    public GameObject projectilePrefab;
    public int maxHealth;
    public int health;
    public float maxDelay;
    public float fireDelay;

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        health = maxHealth;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        MoveTowardPlayer();
        Vector2 distanceToPlayer = Player.Instance.rb.position - rb.position;

        // Decrease fire delay timer
        if (fireDelay > 0)
        {
            fireDelay -= Time.deltaTime;
        }
        else if (distanceToPlayer.magnitude < lookRadius)
        {
            ShootBarrage();
            ShootPlayer();
            fireDelay = maxDelay;
        }
    }

    /// <summary>
    /// Jimmy Williams and Thomas Roman 10/26/2024
    /// A targeted shot
    /// </summary>
    void ShootPlayer()
    {
        bossAudio.Play();
        Vector2 target = Player.Instance.transform.position;
        Vector2 myPos = transform.position;
        Vector2 direction = target - myPos;
        direction.Normalize();
        Quaternion rotation = Quaternion.Euler(0, 0, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg);
        GameObject projectile = Instantiate(projectilePrefab, myPos, rotation);
        Projectile projectileScript = projectile.GetComponent<Projectile>();
        projectileScript.isCritterBullet = true;
        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = direction * bulletSpeed;
        }
    }

    /// <summary>
    /// Jimmy Williams and Thomas Roman 10/26/2024
    /// An omnidirectional attack pattern 
    /// </summary>
    void ShootBarrage()
    {
        for (int i = 1; i <= numberOfBulletsInBarrage; i++)
        {
            Vector2 myPos = transform.position;
            Vector2 direction = new Vector2(Mathf.Cos(2 * Mathf.PI / i), Mathf.Sin(2 * Mathf.PI / i));
            direction.Normalize();
            Quaternion rotation = Quaternion.Euler(0, 0, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg);
            GameObject projectile = Instantiate(projectilePrefab, myPos, rotation);
            Projectile projectileScript = projectile.GetComponent<Projectile>();
            projectileScript.isCritterBullet = true;
            Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.velocity = direction * bulletSpeed;
            }
        }
    }

    /// <summary>
    /// Thomas Roman 10/26/2024
    /// Chases the player if the player is within its sight radius
    /// </summary>
    void MoveTowardPlayer()
    {
        Vector2 distanceToPlayer = Player.Instance.rb.position - rb.position;
        if (distanceToPlayer.magnitude < lookRadius)
        {
            Vector2 movement = distanceToPlayer.normalized * speed;
            rb.velocity = movement;
        }
    }

    /// <summary>
    /// Applies damage to the boss and checks if health has reached zero.
    /// </summary>
    /// <param name="damageAmount">Amount of damage to apply.</param>
    public void TakeDamage(int damageAmount)
    {
        Debug.Log("Damage");
        health -= damageAmount;
        if (health <= 0)
        {
            Die();
        }
    }

    /// <summary>
    /// Handles the boss's death logic.
    /// </summary>
    void Die()
    {
        // You can add death animations, sound effects, or other cleanup logic here
        Destroy(gameObject);
        Debug.Log("Dead");
    }
}
