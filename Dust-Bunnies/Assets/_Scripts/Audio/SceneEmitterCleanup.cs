using UnityEngine;

public class SceneEmitterCleanup : MonoBehaviour
{
    private void OnDestroy()
    {
        AkUnitySoundEngine.StopAll(gameObject);
        AkUnitySoundEngine.UnregisterGameObj(gameObject);
    }
}