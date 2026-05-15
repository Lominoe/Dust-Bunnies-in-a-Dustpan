using UnityEngine;

/// <summary>
/// Game manager for the Sparkler Minigame
/// </summary>
public class SparklerGM : MonoBehaviour
{
    [SerializeField] private SparkMeter sparkMeter;
    [SerializeField] private LighterMovement lighterMovement;

    void Start() {
        Cursor.lockState = CursorLockMode.Locked;

        PhaseOne();

        // events
        sparkMeter.OnPhaseTwoStart += PhaseTwo;
    }

    /// <summary>
    /// Apply pressure to the lighter to start it
    /// </summary>
    private void PhaseOne() {
        sparkMeter.enabled = true;
    }

    /// <summary>
    /// Hold the lighter near the tip of the sparkler to light it
    /// </summary>
    private void PhaseTwo() {
        sparkMeter.enabled = false;
        lighterMovement.SetLighterMovement(true);
    }

    /// <summary>
    /// Trace shapes with the sparkler
    /// </summary>
    private void PhaseThree() {
    }
}
