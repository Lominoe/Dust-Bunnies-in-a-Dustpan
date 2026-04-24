using UnityEngine;

public class OrientToCam : MonoBehaviour
{
    private void LateUpdate() {
        if (Camera.main == null) return;
        
        Vector3 toCamera = (Camera.main.transform.position - transform.position).normalized;
        Quaternion targetRotation = Quaternion.FromToRotation(transform.up, toCamera) * transform.rotation;

        transform.rotation = targetRotation;
    }
}
