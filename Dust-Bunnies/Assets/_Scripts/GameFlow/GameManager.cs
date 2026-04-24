using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public static event System.Action OnLoadNextSnapshot;
    public static event System.Action OnLoadPreviousSnapshot;
    public static event System.Action OnRestartGame;

    public static Vector3 playerCoords;       // used for when the player teleports to a new scene and coords need to be preserved
    public static Quaternion playerRotation;  // ^
    public static bool hasStoredData = false;

    private static int currSnapshotNumber = 1;
    public static int CurrentSnapshotNumber => currSnapshotNumber;
     
    void Awake() {
        if (Instance != null) {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private PlayerController player;
    public static PlayerController Player {
        get => Instance.player;
        set { 
            if (Instance.player == null && value) {
                Instance.player = value;
                // can add an event here that notifies systems that the player has been assigned
            }
        }
    }

    /// <summary>
    /// Store position and rotation based on the players position
    /// relative to the room to be used when they next spawned in
    /// </summary>
    public static void StorePlayerPosition(Vector3 pos, Quaternion rot) {
        playerCoords = pos;
        playerRotation = rot;
        hasStoredData = true;
    }

    //public static void NextSnapshot() {
    //    // will get a request from the player to go to the next snapshot
    //    // first the snapshot requirements will be checked / a way to check if the player has
    //    // completed what they need to to advance
    //    if (!CheckSnapshotRequirements(currSnapshotNumber + 1)) {
    //        return;
    //    }

    //    // if it goes through send an event to the ui to advance
    //    // also send an event to scene loader to load the next scene
    //    OnLoadNextSnapshot?.Invoke();

    //    // also increment the snapshot number
    //    currSnapshotNumber++;
    //}

    //public static void PreviousSnapshot() {
    //    if (!CheckSnapshotRequirements(currSnapshotNumber - 1)) {
    //        return;
    //    }
    //    OnLoadPreviousSnapshot?.Invoke();
    //    currSnapshotNumber--;
    //}

    public static void ChangeSnapshot(int direction) {
        int target = currSnapshotNumber + direction;

        if (!CheckSnapshotRequirements(target)) return;
        currSnapshotNumber = target;

        if (direction > 0)
            OnLoadNextSnapshot?.Invoke();
        else
            OnLoadPreviousSnapshot?.Invoke();
    }

    /// <summary>
    /// Parameter is the snapshot the player is trying to switch to
    /// returns true if you can switch and false if requirements are missing
    /// </summary>
    public static bool CheckSnapshotRequirements(int snapshotNumber) {
        // TODO: jank b*tch
        // will write a switch statement here
        switch (snapshotNumber) {
            case 0:
                return false;   // also no going back to main menu
            case 1:
                return true;   // you can only ever switch to snapshot 1 through the mirror
            default:
                return true;
        }
        //return false;
    }

    public static void RestartGame() {
        Debug.Log("Restart Button Pressed");
        currSnapshotNumber = 0;
        hasStoredData = false;
        OnRestartGame?.Invoke();
    }
}
