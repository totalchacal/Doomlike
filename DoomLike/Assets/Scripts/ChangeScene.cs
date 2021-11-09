using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public static void GoToScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }

    public void GoToSceneWrapper(string scene)
    {
        Time.timeScale = 1.0f;
        ScoreCross.Reset();
        GoToScene(scene);
    }

    public static void GoToRandGame()
    {
        int rand = SceneManager.GetActiveScene().buildIndex;
        
        while (SceneManager.GetActiveScene().buildIndex == rand || System.IO.Path.GetFileNameWithoutExtension(SceneUtility.GetScenePathByBuildIndex(rand)) == "GameOver" || System.IO.Path.GetFileNameWithoutExtension(SceneUtility.GetScenePathByBuildIndex(rand)) == "Menu")
        {
            rand = Random.Range(0, SceneManager.sceneCountInBuildSettings);
        }
        SceneManager.LoadScene(sceneBuildIndex: rand);
    }

    public void GoToRandGameWrapper()
    {
        GoToRandGame();
    }

    public static void QuitGame()
    {
        Application.Quit();
    }

    public void QuitGameWrapper()
    {
        QuitGame();
    }
}
