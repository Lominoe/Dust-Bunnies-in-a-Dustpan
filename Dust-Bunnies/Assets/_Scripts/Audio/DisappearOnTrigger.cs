using UnityEngine;

public class DisappearOnTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
        {
            return;
        }

        gameObject.SetActive(false);
    }
}
