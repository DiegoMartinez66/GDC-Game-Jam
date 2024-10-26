using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class LightCone : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        SyncPositionWithPlayer();
        RotateLightCone();
    }

    /// <summary>
    /// Thomas Roman 10/26/2024
    /// Sets lightcone rotation to player rotation
    /// </summary>
    void RotateLightCone()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.rotation = Quaternion.LookRotation(Vector3.forward, (mousePos - transform.position)) * Quaternion.Euler(0, 0, 90);
    }
    /// <summary>
    /// Thomas Roman 10/26/2024
    /// Sets the position of the lightcone to the players position
    /// </summary>
    void SyncPositionWithPlayer()
    {
        transform.position = Player.Instance.transform.position;
    }
}
