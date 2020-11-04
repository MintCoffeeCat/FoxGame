using UnityEngine;
using System.Collections;

public class PauseMenu : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetButtonDown("Menu"))
        {
            this.Resume();
        }
    }
    public void QuitGame()
    {
        Application.Quit();
    }

    public void Resume()
    {
        this.Hide();
    }

    public void Show()
    {
        this.gameObject.SetActive(true);
        Time.timeScale = 0;
    }

    public void Hide()
    {
        this.gameObject.SetActive(false);
        Time.timeScale = 1;
    }
}
