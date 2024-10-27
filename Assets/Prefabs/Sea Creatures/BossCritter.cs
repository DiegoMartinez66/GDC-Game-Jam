using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // Include SceneManagement for scene loading

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

    void Start()
    {
        Instance = this;
        health = maxHealth;
    }

    void FixedUpdate()
    {
        MoveTowardPlayer();
        Vector2 distanceToPlayer = Player.Instance.rb.position - rb.position;

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

    void MoveTowardPlayer()
    {
        Vector2 distanceToPlayer = Player.Instance.rb.position - rb.position;
        if (distanceToPlayer.magnitude < lookRadius)
        {
            Vector2 movement = distanceToPlayer.normalized * speed;
            rb.velocity = movement;
        }
    }

    public void TakeDamage(int damageAmount)
    {
        health -= damageAmount;
        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        // Load the win scene when the boss is defeated
        SceneManager.LoadScene("WinScreen"); 
    }
}
