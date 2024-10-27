using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Critter : MonoBehaviour
{
    public Sprite[] sprites;
    private int critterIndex;
    public Rigidbody2D rb;
    public SpriteRenderer spriteRenderer;
    public float lookRadius;
    public float frameDuration;
    public float speed;
    private float stunDuration = 0;

    // Start is called before the first frame update
    void Start()
    {
        // a random even index
        critterIndex = 2 * Random.Range(0, (int)Mathf.Floor((sprites.Length-1) / 2));
        spriteRenderer.sprite=sprites[critterIndex];
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        AnimateCritter();
        if (stunDuration > 0)
        {
            // critter is stunned, freeze all movement
            rb.velocity = Vector3.zero;
            stunDuration -= Time.deltaTime;
        } else
        {
            MoveTowardPlayer();
        }

    }
    /// <summary>
    /// Thomas Roman 10/26/2024
    /// Animates the critters
    /// </summary>
    void AnimateCritter()
    {
        // compare time with the duration of each frame
        if (Time.time % frameDuration * 2 > frameDuration)
        {
            spriteRenderer.sprite = sprites[critterIndex + 1];
        }
        else
        {
            spriteRenderer.sprite = sprites[critterIndex];
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
            Vector2 movement = distanceToPlayer.normalized * speed;
            rb.velocity = movement;
        }
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
