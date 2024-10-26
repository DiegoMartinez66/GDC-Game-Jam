using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HealthSystem : MonoBehaviour
{
    //[SerializeField]private 
    private int playerLives = 3;
    private bool IsDead = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (IsDead) 
        {
            SceneManager.LoadScene("LoseScreen");
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("EnemyBullet")) 
        {
            playerLives -= 1;
            Destroy(collision.gameObject);
        }

        if (collision.gameObject.CompareTag("Health"))
        {
            if (playerLives >= 3)
            {
                playerLives = 3;
            }

            else 
            {
                playerLives += 1;
            }
        }
    }
}
