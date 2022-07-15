//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;

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
            {
                pauseWidget.SetActive(true);
                Time.timeScale = 0;
            }
            else
            {
                pauseWidget.SetActive(false);
                Time.timeScale = 1;
            }
        }
    }

    public void ResumeGame()
    {
        pauseWidget.SetActive(false);

        pause = false;
        Time.timeScale = 1;
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
