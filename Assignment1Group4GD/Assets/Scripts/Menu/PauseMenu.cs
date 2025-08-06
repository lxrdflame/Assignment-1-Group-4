using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PauseMenu : MonoBehaviour
{
    
    public GameObject pausePanel;
    public GameObject optionsPanel;
    public GameObject instructionsPanel;

    public GameObject orbManager;
    private CharacterControls playerMovementScript;
    private bool isPaused = false;

    void Start()
    {
        orbManager.SetActive(true);
        pausePanel.SetActive(false);
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("Button pressed!");
            TogglePause();
        }
    }

    public void TogglePause()
    {
        isPaused = !isPaused;
        Time.timeScale = isPaused ? 0 : 1;
        pausePanel.SetActive(isPaused);
        orbManager.SetActive(false);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        if (instructionsPanel.activeInHierarchy)
        {
            pausePanel.SetActive(false);
        }
        
        if (optionsPanel.activeInHierarchy)
        {
            pausePanel.SetActive(false);
        }

        if (pausePanel.activeInHierarchy)
        {
            instructionsPanel.SetActive(false);
            optionsPanel.SetActive(false);
        }

        // Disable player movement
        if (playerMovementScript != null)
        {
            playerMovementScript.enabled = false;
        }


    }

    public void ResumeGame()
    {
        isPaused = false;
        Time.timeScale = 1;
        pausePanel.SetActive(false);

        orbManager.SetActive(true);
        
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        // Re-enable player movement
        if (playerMovementScript != null)
        {
            playerMovementScript.enabled = true;
        }

    }

    public void RestartLevel()
    {

        Time.timeScale = 1f;

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ReturnToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("StartMenu");
    }
}
