using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseGame : MonoBehaviour
{
    public bool paused = false;
    public Canvas pauseScreen;
    public Button resumeButton, restartButton, quitButton;


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
        Cursor.visible = paused;
        Cursor.lockState = paused ? CursorLockMode.None : CursorLockMode.Locked;
    }

    public void TogglePause()
    {
        paused = !paused;
        pauseScreen.enabled = paused;
        Time.timeScale = paused ? 0f : 1f;
    }

    public void Respawn()
    {
        //move player to checkpoint
    }

    public void QuitToMenu()
    {
        TogglePause();
        SceneManager.LoadScene("StartScreen");
    }
}