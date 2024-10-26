using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class Spotlight : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        SetSpotlightPosition();
    }

    /// <summary>
    /// Thomas Roman 10/26/2024
    /// Moves the spotlight to the mouse position
    /// </summary>
    void SetSpotlightPosition()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector3(mousePos.x, mousePos.y, 0.0f);
    }
}
