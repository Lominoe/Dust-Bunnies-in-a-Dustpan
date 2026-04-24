using UnityEngine;
using UnityEngine.VFX;
using Random = UnityEngine.Random;

public class Firework : MonoBehaviour {
    
    [Header("Movement")]
    [SerializeField] private Vector2 travelRange;
    [SerializeField] private Vector2 lifetimeRange;
    [SerializeField] private AnimationCurve movementCurve;
    
    [Space]
    [Header("Supporting Visuals")]
    [SerializeField] private TrailRenderer renderer;
    [SerializeField] private VisualEffect burst;
    [SerializeField] private VisualEffect head;
    [SerializeField] private ImpactFrameController impactController;

    [Space] [Header("Cleanup")] 
    [SerializeField]
    private float cleanupTime = 10f;
    
    [Space]
    [Header("Finale Params")]
    [SerializeField] private bool ignoreFields;

    #region Internals
    private float _travelDistance = 0f;
    private float _lifetime = 0f;
    private float _previousCurveValue = 0f;
    private float _timer = 0f;
    private bool _initialized = false;
    #endregion Internals
    
    public void Activate(Color headColor, Color tailColor) {
        if (impactController != null) impactController.gameObject.SetActive(false);
        _travelDistance = Random.Range(travelRange.x, travelRange.y);
        _lifetime = Random.Range(lifetimeRange.x, lifetimeRange.y);

        _timer = 0f;
        _previousCurveValue = 0f;

        _initialized = true;

        if (ignoreFields) return;
        
        MaterialPropertyBlock mpb = new MaterialPropertyBlock();
        mpb.SetColor("_Head_Color", headColor);
        mpb.SetColor("_Tail_Color", tailColor);
        burst.SetVector4("Burst Color", headColor);
        head.SetVector4("Color", headColor);
        renderer.SetPropertyBlock(mpb);
    }

    private void Update() {
        Step();
    }

    private void Step() {
        if (!_initialized) return;

        _timer += Time.deltaTime;
        
        if (_timer >= _lifetime) {
            Burst();
            return;
        }
        
        float t = Mathf.Clamp01(_timer / _lifetime);

        float curveValue = movementCurve.Evaluate(t);

        float delta = curveValue - _previousCurveValue;
        transform.position += transform.up * (delta * _travelDistance);
        _previousCurveValue = curveValue;
    }

    private void Burst() {
        if (impactController != null) {
            impactController.gameObject.SetActive(true);

            impactController.transform.SetParent(transform.parent);
            impactController.Initiate();
            
            Destroy(impactController.gameObject, cleanupTime);
        }
        
        burst.transform.SetParent(transform.parent);
        burst.gameObject.SetActive(true);

        //Cleanup
        Destroy(burst.gameObject, cleanupTime);
        Destroy(gameObject);
    }
}
