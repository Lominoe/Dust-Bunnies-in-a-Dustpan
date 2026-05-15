using PrimeTween;
using UnityEngine;

public class LighterMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 0.5f;
    private Transform _t;
    public bool CanMoveLighter { get; private set; }
    public void SetLighterMovement(bool b) => CanMoveLighter = b;

    void Start() {
        _t = GetComponent<Transform>();
        CanMoveLighter = false;
    }

    void Update() {
        if (CanMoveLighter) {
            MoveLighter();
        }
    }

    private void MoveLighter() {
        float moveX = Input.GetAxis("Mouse X") * moveSpeed;
        float moveY = Input.GetAxis("Mouse Y") * moveSpeed;

        _t.Translate(new Vector3(moveX, moveY, 0));

        //Vector3 mouseScreen = Input.mousePosition;
        //mouseScreen.z = Mathf.Abs(
        //    Camera.main.transform.position.z - _t.position.z
        //);

        //Vector3 worldPos = Camera.main.ScreenToWorldPoint(mouseScreen);
        //worldPos.x += 11f;
        //worldPos.y += -0.9f;
        //_t.position = worldPos;
    }
}
