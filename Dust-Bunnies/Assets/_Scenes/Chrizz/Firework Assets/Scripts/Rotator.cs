using UnityEngine;

public class Rotator : MonoBehaviour
{
    [Header("Rotation Settings")]
    [SerializeField] private float lifetime = 2f;
    [SerializeField] private float totalRotation = 720f;
    [SerializeField] private AnimationCurve rotationCurve;

    private float _timer = 0f;
    private float _previousCurveValue = 0f;

    private void Update()
    {
        Step();
    }

    private void Step()
    {
        if (_timer >= lifetime) return;

        _timer += Time.deltaTime;
        float t = Mathf.Clamp01(_timer / lifetime);

        float curveValue = rotationCurve.Evaluate(t);

        float delta = curveValue - _previousCurveValue;
        transform.Rotate(Vector3.up, delta * totalRotation, Space.World);

        _previousCurveValue = curveValue;
    }
}