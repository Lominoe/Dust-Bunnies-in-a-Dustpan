using UnityEngine;

public class LockAudio : MonoBehaviour
{
    [SerializeField] private AK.Wwise.Event firstTryAudio;
    [SerializeField] private AK.Wwise.Event otherTryAudio;

    private bool firstTry = true;
    private void OnEnable() {
        GameManager.OnLevelIsLocked += PlayLock;
         
    }

    private void OnDisable() {
        GameManager.OnLevelIsLocked -= PlayLock;
    }

    private void PlayLock() {
        if (firstTry) {
            firstTryAudio?.Post(gameObject);
            firstTry = false;
        } else {
            otherTryAudio?.Post(gameObject);
        }
    }

    private void Unlocked() {
        firstTry = true;
    }
}
