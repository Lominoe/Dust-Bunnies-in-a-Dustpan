using UnityEngine;

[RequireComponent(typeof(Camera))]
public class ImpactCamera : MonoBehaviour
{
    public Camera ThisCamera { get; private set; }
    private Camera _mainCam;
    
    public static ImpactCamera Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        _mainCam = Camera.main;
        ThisCamera = GetComponent<Camera>();
    }

    private void LateUpdate() {
        MatchMainCam();
    }

    private void MatchMainCam() {
        if (ThisCamera == null || _mainCam == null) return;
        transform.position = _mainCam.transform.position;
        transform.rotation = _mainCam.transform.rotation;
        ThisCamera.fieldOfView = _mainCam.fieldOfView;
    }
}
