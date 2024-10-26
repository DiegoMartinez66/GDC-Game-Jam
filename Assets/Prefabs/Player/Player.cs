using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class Player : MonoBehaviour
{
    static public Player Instance;
    public Rigidbody2D rb;
    public SpriteRenderer spriteRenderer;
    public GameObject spotlight;
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        MovePlayer();
        RotatePlayer();
        SetSpotlightPosition();
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
        rb.velocity = movement * speed;
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

    /// <summary>
    /// Thomas Roman 10/26/2024
    /// Moves the spotlight to the mouse position
    /// </summary>
    void SetSpotlightPosition()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        spotlight.transform.position = new Vector3(mousePos.x, mousePos.y, 0.0f);
    }
}
