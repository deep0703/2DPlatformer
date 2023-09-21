using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // For scene management

public class LevelManager : MonoBehaviour
{
    public static LevelManager main;

    public Transform[] path; // enemy path
    public Transform startPoint; // start point

    [Header("Game Over UI")]
    public GameObject gameOverPanel; // Assign your GameOverPanel in the inspector

    private void Awake()
    {
        main = this;
    }

    public void GameOver()
    {
        gameOverPanel.SetActive(true); // Show the game over panel
    }

}
