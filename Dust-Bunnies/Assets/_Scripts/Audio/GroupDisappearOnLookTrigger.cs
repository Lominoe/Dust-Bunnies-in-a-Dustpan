using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Collider))]
public class GroupDisappearOnLookTrigger : MonoBehaviour
{
    [SerializeField] private GameObject groupToHide;
    [SerializeField] private Transform requiredLookDirection;
    [SerializeField] private float requiredLookSeconds = 5f;
    [SerializeField] private float maxLookAngle = 15f;
    [SerializeField] private AK.Wwise.Switch lineSwitch;
    [SerializeField] private AK.Wwise.Event voiceLineEvent;
    [SerializeField] private float disappearDelay;

    private bool playerInside;
    private bool hasTriggered;
    private float lookTimer;
    private Transform playerCamera;

    private void Awake()
    {
        GetComponent<Collider>().isTrigger = true;
    }

    private void Update()
    {
        if (!playerInside || hasTriggered)
        {
            return;
        }

        if (IsLookingInRequiredDirection())
        {
            lookTimer += Time.deltaTime;

            if (lookTimer >= requiredLookSeconds)
            {
                TriggerDisappear();
            }
        }
        else
        {
            lookTimer = 0f;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!TryGetPlayerCamera(other, out playerCamera))
        {
            return;
        }

        playerInside = true;
        lookTimer = 0f;
    }

    private void OnTriggerExit(Collider other)
    {
        if (!TryGetPlayerCamera(other, out _))
        {
            return;
        }

        playerInside = false;
        lookTimer = 0f;
        playerCamera = null;
    }

    private bool IsLookingInRequiredDirection()
    {
        if (requiredLookDirection == null || playerCamera == null)
        {
            return false;
        }

        float angle = Vector3.Angle(playerCamera.forward, requiredLookDirection.forward);
        return angle <= maxLookAngle;
    }

    private bool TryGetPlayerCamera(Collider other, out Transform cameraTransform)
    {
        cameraTransform = null;

        if (!other.CompareTag("Player") && other.GetComponentInParent<PlayerController>() == null)
        {
            return false;
        }

        Camera camera = other.GetComponentInChildren<Camera>();

        if (camera == null)
        {
            PlayerController player = other.GetComponentInParent<PlayerController>();
            camera = player != null ? player.GetComponentInChildren<Camera>() : null;
        }

        if (camera == null)
        {
            camera = Camera.main;
        }

        if (camera == null)
        {
            return false;
        }

        cameraTransform = camera.transform;
        return true;
    }

    private void TriggerDisappear()
    {
        hasTriggered = true;
        lineSwitch?.SetValue(gameObject);
        voiceLineEvent?.Post(gameObject);
        StartCoroutine(HideGroupAfterDelay());
    }

    private IEnumerator HideGroupAfterDelay()
    {
        yield return new WaitForSeconds(disappearDelay);

        if (groupToHide != null)
        {
            groupToHide.SetActive(false);
        }
    }
}
