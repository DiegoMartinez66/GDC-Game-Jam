using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WinLoseScreen : MonoBehaviour
{
    [SerializeField] private string firstLevelName = "SampleScene";
    public Button restartScreen;
    public Button quitButton;
    [SerializeField] private TextMeshProUGUI loading;

    private void Awake()
    {
        restartScreen.onClick.AddListener(LoadFirstLevel);
        quitButton.onClick.AddListener(Quit);
        loading.text = "";
    }
    public void LoadFirstLevel()
    {
        LoadLevel(firstLevelName);
    }


    public void LoadLevel(string levelName)
    {
        loading.text = ("Loading...");
        SceneManager.LoadScene(levelName);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
