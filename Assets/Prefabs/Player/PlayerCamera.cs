using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    public float cameraFollowSpeed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        FollowPlayer();
    }

    /// <summary>
    /// Thomas Roman 10/26/2024
    /// Smoothly moves the camera to the player's position
    /// </summary>
    void FollowPlayer()
    {
        Vector3 targetPosition = new Vector3(Player.Instance.transform.position.x, Player.Instance.transform.position.y, transform.position.z);
        transform.position = Vector3.Slerp(transform.position, targetPosition, Time.deltaTime * cameraFollowSpeed);
    }
}
