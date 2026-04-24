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

        InputReader reader = Object.FindFirstObjectByType<InputReader>();
        if (reader != null) {
            reader.SetEnabled(false);
        }
    }
    public void Resume()
    {
        gameObject.GetComponentInChildren<Canvas>().enabled = false;
        Time.timeScale = 1f;

        InputReader reader = Object.FindFirstObjectByType<InputReader>();
        if (reader != null){
            reader.SetEnabled(true);
        }
    }
    public void Restart()
    {
        //reset game manager and journal (probably need custom function in Journal for hard reset
        //Debug.LogWarning("Instantiation not complete: must hard-reset journal and game manager (when it exists)");
        GameManager.RestartGame();
        Time.timeScale = 1f;
    }
    public void QuitHome()
    {
        Resume();
        //FindFirstObjectByType<SceneFader>().FadeTo(0); //needs to go to title scene
        Debug.LogWarning("Quit to title: Not instantiated yet, no title scene in SceneFader");
    }
}
