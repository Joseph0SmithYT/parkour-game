using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelFinish : MonoBehaviour
{
    public GameManager gameManager;
    
    private void OnTriggerEnter(Collider other)
    {
        gameManager.NextLevel(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
