using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossCritter : MonoBehaviour
{
    static public BossCritter Instance;
    public Rigidbody2D rb;
    public SpriteRenderer spriteRenderer;
    public float speed;
    public float bulletSpeed = 15;
    public float lookRadius;
    public GameObject projectilePrefab;
    public int maxHealth;
    public int health;
    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        MoveTowardPlayer();
    }
    /// <summary>
    /// Thomas Roman 10/26/2024
    /// A targeted shot
    /// </summary>
    void ShootPlayer()
    {

    }
    /// <summary>
    /// Thomas Roman 10/26/2024
    /// An omnidirectional attack pattern 
    /// </summary>
    void ShootBarrage()
    {

    }

    /// <summary>
    /// Thomas Roman 10/26/2024
    /// Chases the player if the player is within its sight radius
    /// </summary>
    void MoveTowardPlayer()
    {
        // calculate the vector to the player
        Vector2 distanceToPlayer = Player.Instance.rb.position - rb.position;
        if (distanceToPlayer.magnitude < lookRadius)
        {
            Vector2 movement = distanceToPlayer.normalized * speed;
            rb.velocity = movement;
        }
    }
}
