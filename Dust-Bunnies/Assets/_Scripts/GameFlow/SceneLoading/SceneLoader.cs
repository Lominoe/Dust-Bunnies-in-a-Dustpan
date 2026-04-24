using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] private SceneFader sceneFader;

    void Start()
    {
        GameManager.OnLoadNextSnapshot += LoadNextScene;
        GameManager.OnLoadPreviousSnapshot += LoadPreviousScene;
        GameManager.OnRestartGame += RestartGame;
    }

    public void LoadNextScene()
    {
        int index = SceneManager.GetActiveScene().buildIndex + 1;
        StartCoroutine(LoadScene(index));
        Cleanup();
    }

    public void LoadPreviousScene() {
        int index = SceneManager.GetActiveScene().buildIndex - 1;
        StartCoroutine(LoadScene(index));
        Cleanup();
    }
    public void RestartGame() {
        StartCoroutine(LoadScene(0));
        Cleanup();
    }

    private void Cleanup() {
        GameManager.OnLoadNextSnapshot -= LoadNextScene;
        GameManager.OnLoadPreviousSnapshot -= LoadPreviousScene;
        GameManager.OnRestartGame -= RestartGame;
    }

    private IEnumerator LoadScene(int index)
    {
        yield return StartCoroutine(sceneFader.FadeOut());

        SceneManager.LoadScene(index);
    }
}
