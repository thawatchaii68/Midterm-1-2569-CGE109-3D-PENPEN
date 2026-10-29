using UnityEngine;

public class ScriptPauseManager : MonoBehaviour
{
    public GameObject pauseMenuUI;
    private bool isPaused = false;

    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }
    public void Resume()
    {
        if (pauseMenuUI != null)
            pauseMenuUI.SetActive(false); 

        Time.timeScale = 1f; 
        isPaused = false;

        ScriptGameManager.Instance?.SetPaused(false);
    }

    public void Pause()
    {
        if (pauseMenuUI != null)
            pauseMenuUI.SetActive(true); 
        Time.timeScale = 0f; 
        isPaused = true;

        
        ScriptGameManager.Instance?.SetPaused(true);
    }
}