using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    private void Awake()
    {
        PlayerPrefs.SetInt("Difficulty", 0);
    }

    public void SetDifficulty(string difficulty)
    {
        if(difficulty == "Easy")
        {
            PlayerPrefs.SetInt("Difficulty", 0);
        }
        if (difficulty == "Medium")
        {
            PlayerPrefs.SetInt("Difficulty", 1);
        }
        if (difficulty == "Hard")
        {
            PlayerPrefs.SetInt("Difficulty", 2);
        }
    }
    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }
}
