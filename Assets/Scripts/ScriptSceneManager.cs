using UnityEngine;
using UnityEngine.SceneManagement;

public class ScriptSceneManager : MonoBehaviour
{
    public const string GameScene = "Game";
    public const string MainMenuScene = "Mainmenu";

    public void LoadScene(string sceneName)
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(sceneName);
    }

    public void PlayGame() => LoadScene(GameScene);
    public void GoToMainMenu() => LoadScene(MainMenuScene);
    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void QuitGame()
    {
        Debug.Log("Quit game"); 
        Application.Quit();    
    }
}