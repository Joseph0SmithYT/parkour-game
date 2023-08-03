using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public GameObject levelsPanel;
    public GameObject mainPanel;

    public void levelsPanelActivate(GameObject gameObject) {
        StopAllPanels();
        gameObject.SetActive(!gameObject.activeSelf);
    }
    public void LoadScene(int sceneIndex) {
        SceneManager.LoadScene(sceneIndex);
    }
    public void StopAllPanels()
    {
        levelsPanel.SetActive(false);
        mainPanel.SetActive(false);
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
        StopAllPanels();
        mainPanel.SetActive(true);
    }

    
}
