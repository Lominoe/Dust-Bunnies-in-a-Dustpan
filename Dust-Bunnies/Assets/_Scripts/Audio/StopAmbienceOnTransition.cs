using UnityEngine;

public class StopAmbienceOnTransition : MonoBehaviour
{
    [SerializeField] private AK.Wwise.Event stopAmbienceEvent;

    private void OnEnable()
    {
        GameManager.OnLoadNextSnapshot += StopAmbience;
        GameManager.OnLoadPreviousSnapshot += StopAmbience;
        GameManager.OnRestartGame += StopAmbience;
    }

    private void OnDisable()
    {
        GameManager.OnLoadNextSnapshot -= StopAmbience;
        GameManager.OnLoadPreviousSnapshot -= StopAmbience;
        GameManager.OnRestartGame -= StopAmbience;
    }

    private void StopAmbience()
    {
        stopAmbienceEvent?.Post(gameObject);
    }
}