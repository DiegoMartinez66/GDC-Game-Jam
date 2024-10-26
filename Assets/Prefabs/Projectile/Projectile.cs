using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private float speed;
    private Rigidbody rb;
    // Update is called once per frame
    void Update()
    {
        //transform.Translate(Vector2.up * speed * Time.deltaTime);

        Destroy(gameObject, 5f);
    }
}
