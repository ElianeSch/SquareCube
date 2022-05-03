using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{



    public void StartGame()
    {
        SceneManager.LoadScene("Niveau1");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void LoadCredits()
    {
        print("Load Credits");
    }



}
