using UnityEngine;

[RequireComponent(typeof(Collider))]
public class SetWwiseStateOnTrigger : MonoBehaviour
{
    [SerializeField] private AK.Wwise.State state;

    private void Awake()
    {
        GetComponent<Collider>().isTrigger = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player") && other.GetComponentInParent<PlayerController>() == null)
        {
            return;
        }

        state?.SetValue();
    }
}