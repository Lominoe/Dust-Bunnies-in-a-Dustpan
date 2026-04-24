using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class DelayedTriggerActivation : MonoBehaviour
{
    [SerializeField] private float activationDelay = 5f;

    private Collider triggerCollider;

    private void Awake()
    {
        triggerCollider = GetComponent<Collider>();
        triggerCollider.isTrigger = true;
        triggerCollider.enabled = false;
    }

    private void OnEnable()
    {
        StartCoroutine(EnableTriggerAfterDelay());
    }

    private IEnumerator EnableTriggerAfterDelay()
    {
        yield return new WaitForSeconds(activationDelay);
        triggerCollider.enabled = true;
    }
}
