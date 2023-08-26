using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public GameObject levelsPanel;
    public GameObject mainPanel;
    public GameManager GameManager;
    public GameObject pauseMenu;
    public static bool gamePaused = false;

    public void panelActivate(GameObject gameObject) {
        StopAllPanels();
        gameObject.SetActive(!gameObject.activeSelf);
    }
    public void LoadScene(int sceneIndex) {
        StopAllPanels();
        Time.timeScale = 1f;
        GameManager.NextLevel(sceneIndex);
    }
    public void StopAllPanels()
    {
        if (levelsPanel != null)
        {
            levelsPanel.SetActive(false);
        }

        if (mainPanel != null)
        {
            mainPanel.SetActive(false);
        }

        if (pauseMenu != null)
        {
            pauseMenu.SetActive(false);
        }
        Cursor.lockState = CursorLockMode.Locked;
    }
    public void Quit()
    {
        Application.Quit();
        #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
    private void Start()
    {
        if (SceneManager.GetActiveScene().buildIndex == 0) { Cursor.lockState = CursorLockMode.None; }
        StopAllPanels();
        if (SceneManager.GetActiveScene().buildIndex == 0) mainPanel.SetActive(true);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (gamePaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
            /**/
            //LoadScene(0);
        }
    }

    public void Pause()
    {
        Cursor.lockState = CursorLockMode.None;
        Time.timeScale = 0f;
        pauseMenu.SetActive(true);
        gamePaused = true;
    }
    public void Resume()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1f;
        pauseMenu.SetActive(false);
        gamePaused = false;
    }


}
