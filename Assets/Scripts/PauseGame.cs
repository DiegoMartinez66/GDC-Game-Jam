using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseGame : MonoBehaviour
{
    public bool paused = false;
    public Canvas pauseScreen;
    public Button resumeButton;
    public Button restartButton;
    public Button quitButton;
    public GameObject car;


    // Start is called before the first frame update
    void Awake()
    {
        paused = false;
        pauseScreen.enabled = paused;

        // Add listener to the resume button to call TogglePause when clicked
        resumeButton.onClick.AddListener(TogglePause);

        // Optionally, you can add listeners for other buttons as well:
        restartButton.onClick.AddListener(Respawn);
        quitButton.onClick.AddListener(QuitToMenu);
    }

    // Update is called once per frame
    void Update()
    {
        pauseScreen.enabled = paused;
        //pauses game
#if (UNITY_EDITOR)
        if (Input.GetKeyDown(KeyCode.P))
        {
            TogglePause();
        }
#else
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
#endif
        if (paused)
        {
            //only shows mouse if game is paused
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            //doesnt work if escape is hit to unpause bc unity editor shenanigans
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }


    /// <summary>
    /// toggles and shows pause screen
    /// </summary>
    /// <returns></returns>
    public void TogglePause()
    {
        paused = !paused;
        pauseScreen.enabled = paused;
        if (paused == true)
        {
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1f;
        }
    }

    public void Respawn()
    {
        // Reset the car's position to the origin (or any specific respawn point)
        car.transform.position = Vector3.zero;

        // Reset the car's rotation to zero (upright position)
        car.transform.rotation = Quaternion.identity;

        // Optionally, reset the car's Rigidbody velocity to prevent it from continuing its motion after respawn
        Rigidbody carRigidbody = car.GetComponent<Rigidbody>();
        if (carRigidbody != null)
        {
            carRigidbody.velocity = Vector3.zero;
            carRigidbody.angularVelocity = Vector3.zero;
        }

        // Unpause the game if needed
        paused = false;
        Time.timeScale = 1f;
        pauseScreen.enabled = paused;
    }

    public void QuitToMenu()
    {
        paused = false;
        Time.timeScale = 1f;
        pauseScreen.enabled = paused;
        SceneManager.LoadScene("StartScreen");
    }
}