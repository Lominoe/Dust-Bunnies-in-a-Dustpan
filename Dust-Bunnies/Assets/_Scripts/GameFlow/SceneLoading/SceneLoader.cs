using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] private SceneFader sceneFader;

    void Start()
    {
        GameManager.OnLoadNextSnapshot += LoadNextScene;
    }

    public void LoadNextScene()
    {
        int index = SceneManager.GetActiveScene().buildIndex + 1;
        StartCoroutine(NextScene(index));
        GameManager.OnLoadNextSnapshot -= LoadNextScene;
    }

    private IEnumerator NextScene(int index)
    {
        yield return StartCoroutine(sceneFader.FadeOut());

        switch (index)
        {
            case 0:
                AkUnitySoundEngine.SetState("Scene", "Snapshot0");
                break;

            case 1:
                AkUnitySoundEngine.SetState("Scene", "Snapshot1");
                break;

            case 2:
                AkUnitySoundEngine.SetState("Scene", "Snapshot2");
                break;

            case 3:
                AkUnitySoundEngine.SetState("Scene", "Snapshot3");
                break;
            case 4:
                AkUnitySoundEngine.SetState("Scene", "Snapshot4");
                break;

            case 5:
                AkUnitySoundEngine.SetState("Scene", "Snapshot5");
                break;

            case 6:
                AkUnitySoundEngine.SetState("Scene", "Snapshot6");
                break;

            case 7:
                AkUnitySoundEngine.SetState("Scene", "Snapshot7");
                break;

            case 8:
                AkUnitySoundEngine.SetState("Scene", "Snapshot8");
                break;

            case 9:
                AkUnitySoundEngine.SetState("Scene", "Snapshot9");
                break;

            case 10:
                AkUnitySoundEngine.SetState("Scene", "Snapshot10");
                break;

            case 11:
                AkUnitySoundEngine.SetState("Scene", "Snapshot11");
                break;

            case 12:
                AkUnitySoundEngine.SetState("Scene", "Snapshot12");
                break;

            default:
                break;
        }
        SceneManager.LoadScene(index);
    }
}
