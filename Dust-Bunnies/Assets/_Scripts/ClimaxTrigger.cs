using UnityEngine;

public class ClimaxTrigger : MonoBehaviour
{
    [SerializeField] private FireworkSpawner fireworkSpawner;
    private bool hasEntered = false;

    void OnTriggerEnter(Collider other) {
        if (hasEntered) { return; }

        fireworkSpawner.PlayFinale();
        hasEntered = true;
    }
}
