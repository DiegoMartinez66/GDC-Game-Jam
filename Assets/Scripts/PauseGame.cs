using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseGame : MonoBehaviour
{
    public bool paused = false;
    public Canvas pauseScreen;
    public Button resumeButton, restartButton, quitButton;
    public GameObject player;


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
        if (Input.GetKeyDown(KeyCode.Escape)||Input.GetKeyDown(KeyCode.P))
        {
            TogglePause();
        }
#endif
        UpdateCursorState();
    }


    /// <summary>
    /// toggles and shows pause screen
    /// </summary>
    /// <returns></returns>
    void UpdateCursorState()
    {
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
            //Cursor.lockState = CursorLockMode.Locked;
        }
    }

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
        TogglePause();
        player.GetComponent<Player>().Respawn();
    }

    public void QuitToMenu()
    {
        TogglePause();
        UpdateCursorState();
        SceneManager.LoadScene("StartScreen");
    }
}