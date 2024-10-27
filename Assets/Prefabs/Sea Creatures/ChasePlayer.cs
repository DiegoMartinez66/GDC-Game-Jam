using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChasePlayer : MonoBehaviour
{
    public Rigidbody2D rb;
    public SpriteRenderer spriteRenderer;
    public float lookRadius;
    public float lookSpeed;
    public float speed;
    private float stunDuration = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (stunDuration > 0)
        {
            // critter is stunned, freeze all movement
            rb.velocity = Vector3.zero;
            stunDuration -= Time.deltaTime;
        }
        else
        {
            MoveTowardPlayer();
        }
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
            LookAtPlayer();
            Vector2 movement = distanceToPlayer.normalized * speed;
            rb.velocity = movement;
        }
    }

    /// <summary>
    /// Thomas Roman 10/26/2024
    /// Smoothly rotates creature to face in the direction of the player
    /// </summary>
    void LookAtPlayer()
    {
        Quaternion targetRotation = Quaternion.LookRotation(Vector3.forward, (Player.Instance.transform.position - transform.position));

        // flip creature if player is to the left
        if (Player.Instance.transform.position.x < transform.position.x)
        {
            targetRotation *= Quaternion.Euler(new Vector3(0, 0, -90));
            if (spriteRenderer.flipX == false)
            {
                spriteRenderer.flipX = true;
                // immediately set the rotation on flip to prevent weird/buggy rotations
                transform.rotation = targetRotation;
            }
        }
        else
        {
            targetRotation *= Quaternion.Euler(new Vector3(0, 0, 90));
            if (spriteRenderer.flipX == true)
            {
                spriteRenderer.flipX = false;
                // immediately set the rotation on flip to prevent weird/buggy rotations
                transform.rotation = targetRotation;
            }
        }
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * lookSpeed);
    }

    /// <summary>
    /// Thomas Roman 10/26/2024
    /// Prevents the critter from moving for a bit
    /// </summary>
    public void Stun(float stunLength)
    {
        this.stunDuration = stunLength;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<Player>().TakeDamage(1);
        }
    }
}
