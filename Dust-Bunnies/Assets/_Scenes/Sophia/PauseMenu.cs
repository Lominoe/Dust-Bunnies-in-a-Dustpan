using System.IO;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    private PauseMenu instance;

    //temporary, doesn't go thru input control
    // TODO: will def fix this but it works for the final demo tbh lmao
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Pause();
        }
    }
    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        gameObject.GetComponentInChildren<Canvas>().enabled = false;
    }
    public void Pause()
    {
        gameObject.GetComponentInChildren<Canvas>().enabled = true;
        Time.timeScale = 0f;
        if (Object.FindFirstObjectByType<InputReader>() != null) 
            Object.FindFirstObjectByType<InputReader>().SetEnabled(false);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
    public void Resume()
    {
        gameObject.GetComponentInChildren<Canvas>().enabled = false;
        Time.timeScale = 1f;
        if (Object.FindFirstObjectByType<InputReader>() != null)
            Object.FindFirstObjectByType<InputReader>().SetEnabled(true);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
    public void Restart()
    {
        //reset game manager and journal (probably need custom function in Journal for hard reset
        //Debug.LogWarning("Instantiation not complete: must hard-reset journal and game manager (when it exists)");
        Time.timeScale = 1f;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        GameManager.RestartGame();
    }
    public void QuitHome()
    {
        Resume();
        //FindFirstObjectByType<SceneFader>().FadeTo(0); //needs to go to title scene
        Debug.LogWarning("Quit to title: Not instantiated yet, no title scene in SceneFader");
    }
}
