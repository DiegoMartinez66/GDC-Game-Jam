using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Animations;

public class Player : MonoBehaviour
{
    public AudioSource AudioClip;
    static public Player Instance;
    public Rigidbody2D rb;
    public SpriteRenderer spriteRenderer;
    public float speed;
    public float altSpeed;
    public float bulletSpeed=15;
    public GameObject projectilePrefab;
    public TextMeshProUGUI textMesh;
    public int maxHealth;
    public int health;
    public Transform shootPoint;
    public Vector3 respawnCords;
    private float invincibilityDuration = 0;
    private float invincibilityTime = 3.0f;  // 3 seconds of invincibility

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        UpdateHealth();
        respawnCords = this.transform.position;
        health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        MovePlayer();
        if (Time.timeScale > 0) {
            RotatePlayer();
            Shoot();
}
        UpdateHealth();

        if (invincibilityDuration > 0)
        {
            invincibilityDuration -= Time.deltaTime;
        }
    }
    /// <summary>
    /// Thomas Roman 10/26/2024
    /// Handles player movement based on keyboard input
    /// </summary>
    void MovePlayer()
    {
        // set the players velocity to the input direction times the speed
        Vector2 movement = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        movement.Normalize();
        if (Input.GetKey(KeyCode.LeftShift))
        {
            rb.velocity = movement * altSpeed;
        } else
        {
            rb.velocity = movement * speed;
        }
    }
    /// <summary>
    /// Thomas Roman 10/26/2024
    /// Rotates player to face in the direction of the mouse
    /// </summary>
    void RotatePlayer()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.rotation = Quaternion.LookRotation(Vector3.forward, (mousePos - transform.position));

        // flip player if mouse is to the left
        if (mousePos.x < transform.position.x)
        {
            spriteRenderer.flipX = true;
            transform.Rotate(new Vector3(0, 0, -90));

        } else
        {
            spriteRenderer.flipX= false;
            transform.Rotate(new Vector3(0, 0, 90));
        }
    }
    void Shoot()
    {
        if (Input.GetMouseButtonDown(0))
        {
            AudioClip.Play();

            Vector2 target = Camera.main.ScreenToWorldPoint(new Vector2(Input.mousePosition.x, Input.mousePosition.y));
            Vector2 myPos = shootPoint.position;
            Vector2 direction = target - myPos;
            direction.Normalize();
            Quaternion rotation = Quaternion.Euler(0, 0, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg);
            GameObject projectile = Instantiate(projectilePrefab, myPos, rotation);
            Projectile projectileScript = projectile.GetComponent<Projectile>();
            projectileScript.isCritterBullet = false;
            Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.velocity = direction * bulletSpeed;
            }
        }
    }

    public void TakeDamage(int damage)
    {
        if(invincibilityDuration > 0)
        {
            return;
        }
        health -= damage;
        if (health <= 0)
        {
            Debug.Log("Taken Damage");
            Respawn();
        }
        else
        {
            invincibilityDuration = invincibilityTime;  // Start invincibility period
        }
    }

    void UpdateHealth()
    {
        textMesh.text = "Health: " + health.ToString();
    }

    public void Respawn()
    {
        this.transform.position = respawnCords;
        this.health = maxHealth;
        invincibilityDuration = invincibilityTime;
    }
}
