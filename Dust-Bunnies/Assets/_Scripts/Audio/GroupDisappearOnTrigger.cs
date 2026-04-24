using UnityEngine;

public class GroupDisappearOnTrigger : MonoBehaviour
{
    [SerializeField] private GameObject groupToHide;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
        {
            return;
        }

        groupToHide.SetActive(false);
    }
}
