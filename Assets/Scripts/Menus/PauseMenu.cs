using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour {

    public GameObject pausePanel;
    public static bool gamePaused = false;

    void Update()
    {
        if (!DebugManager.instance.debug && Input.GetButtonDown("Pause"))
        {
            if (gamePaused)
                Resume();
            else
                Pause();
        }
    }

    public void Resume()
    {
        pausePanel.SetActive(false);
        Time.timeScale = 1f;
        gamePaused = false;
    }

    public void Pause()
    {
        pausePanel.SetActive(true);
        Time.timeScale = 0f;
        gamePaused = true;
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}