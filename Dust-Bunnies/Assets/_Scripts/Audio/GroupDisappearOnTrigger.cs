using UnityEngine;

public class GroupDisappearOnTrigger : MonoBehaviour
{
    [SerializeField] private GameObject groupToHide;

    private void OnTriggerEnter(Collider other)
    {
        groupToHide.SetActive(false);
    }
}