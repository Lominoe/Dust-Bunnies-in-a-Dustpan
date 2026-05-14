using System.Collections;
using UnityEngine;

public class ClimaxTrigger : MonoBehaviour
{
    [SerializeField] private FireworkSpawner fireworkSpawner;
    [SerializeField] private float delayTime = 0f;
    private bool hasEntered = false;

    void OnTriggerEnter(Collider other) {
        if (hasEntered) { return; }

        StartCoroutine(WaitForSeconds(delayTime));      // if this doesn't work mb - Jazz Man
        hasEntered = true;
    }

    private IEnumerator WaitForSeconds(float seconds) {
        yield return new WaitForSeconds(seconds);
        fireworkSpawner.PlayFinale();
    }
}
