using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class GameManager
{
    public static void NextLevel()
    {
        int activeSceneIndex = SceneManager.GetActiveScene().buildIndex;
        if (CheckSceneExists(activeSceneIndex + 1))
        {
            SceneManager.LoadScene(activeSceneIndex + 1);
        }
        else SceneManager.LoadScene(0);
    }

    // Start is called before the first frame update

    static public bool CheckSceneExists(int buildIndex)
    {
        if (buildIndex >= 0 && buildIndex < SceneManager.sceneCountInBuildSettings)
        {
            return true; // Scene exists
        }
        else
        {
            return false; // Scene does not exist
        }
    }
}