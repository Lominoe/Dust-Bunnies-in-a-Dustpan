using PrimeTween;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// Base Object for everything that can be interacted with.
/// </summary>
[RequireComponent(typeof(Collider))]
public class PickupAble : Interactable {
    [SerializeField] private AK.Wwise.Event pickupEvent;
    [SerializeField] private AK.Wwise.Event putDownEvent;
    [SerializeField] private AK.Wwise.Switch pickupSwitch;
    [SerializeField] private AK.Wwise.Switch putDownSwitch;
    [SerializeField] private AK.Wwise.State pickupState;
    [SerializeField] private AK.Wwise.State putDownState;

    private Vector3 _startPos;  // store initial start pos to return back to
    private Quaternion _startRot;
    private Collider _collider;

    public Vector3 StartPos => _startPos;
    public Quaternion StartRot => _startRot;

    private Transform t;

    void Start() {
        _startPos = transform.position;
        _startRot = transform.rotation;
        _collider = this.GetComponent<Collider>();
        t = transform;
    }

    public override void Interact(Transform playerCam, float moveTime) {
        base.Interact(playerCam, moveTime);
        pickupSwitch?.SetValue(gameObject);
        pickupEvent?.Post(gameObject);
        pickupState?.SetValue();
        StartCoroutine(PickUp(playerCam, moveTime));
    }

    public override void InteractEnd(float moveTime) {
        base.InteractEnd(moveTime);
        PutDown(moveTime);
    }

    public override PlayerState GetNextState(PlayerController p, InputReader i) {
        base.GetNextState(p, i);
        return new PickUpState(p, i);
    }

    private IEnumerator PickUp(Transform playerCam, float moveTime) {

        float longestBound = _collider.bounds.size.x > _collider.bounds.size.y      // use the objects longer edge to calculate hold distance
            ? _collider.bounds.size.x : _collider.bounds.size.y;

        float holdDistance = 0.5f + longestBound;                                      // the distance from the camera the object should be held. Smaller items are held closer
        Vector3 holdPoint = playerCam.position + playerCam.forward * holdDistance;  // recalc the hold point

        //holdPoint += -playerCam.up * (_collider.bounds.size.y / 2);

        // lets do this scuff first
        Tween.Position(t, holdPoint, moveTime, Ease.InSine);    // move to player hold point
        Debug.DrawLine(playerCam.position, holdPoint, Color.red, 10f);

        // calculate rotation to point towards player cam
        Quaternion rot = Quaternion.LookRotation(t.position - playerCam.position, Vector3.up);
        Tween.Rotation(t, rot, moveTime, Ease.InSine);

        yield return new WaitForSeconds(moveTime);
    }

    public virtual void PutDown(float moveTime) {
        putDownSwitch?.SetValue(gameObject);
        putDownState?.SetValue();
        putDownEvent?.Post(gameObject);

        Tween.Position(t, StartPos, moveTime, Ease.OutSine);
        Tween.Rotation(t, StartRot, moveTime, Ease.OutSine);

        // if there is a requirement for picking up this item, notify the game manager
        if (requirement != null) {
            if (requirement.snapshotNumber > 0) {
                GameManager.ReportUnlock(requirement.condition, requirement.conditionID);
            }
        }
    }
}
