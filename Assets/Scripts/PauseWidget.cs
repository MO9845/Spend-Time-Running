//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseWidget : MonoBehaviour
{
    private bool pause;

    public GameObject pauseWidget;

    void Start()
    {
        pause = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            pause = !pause;

            if (pause)
                PauseGame();
            else
                ResumeGame();
        }
    }

    public void PauseGame()
    {
        pauseWidget.SetActive(true);

        pause = true;
        Time.timeScale = 0;
    }

    public void ResumeGame()
    {
        pauseWidget.SetActive(false);

        pause = false;
        Time.timeScale = 1;
    }

    public void QuitGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Main");
    }

    void OnApplicationPause(bool pause)
    {
        if (pause)
            PauseGame();
    }
}
