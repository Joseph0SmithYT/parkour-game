using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    #region Level
    public Animator transition;
    public void NextLevel(int sceneIndex)
    { 
        if (CheckSceneExists(sceneIndex))
        {
            StartCoroutine(NextLevelCoroutine(sceneIndex));
        }
        else StartCoroutine(NextLevelCoroutine(0));
    }

    // Start is called before the first frame update
    IEnumerator NextLevelCoroutine(int activeSceneIndex)
    {
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(2f);
        Debug.Log(activeSceneIndex);
        SceneManager.LoadScene(activeSceneIndex);
    }

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
    #endregion
}